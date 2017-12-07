// Fill out your copyright notice in the Description page of Project Settings.

//#include "IdeaSisSocketWrapper.h"

#include "IdeaSisSocketPlugInPrivatePCH.h"

using namespace std;


void IdeaSisSocketWrapper::StartListening(const TCHAR* socketName, const TCHAR* ip, const int32 port, ISocketCallback* callback, const int32 bufferSize, bool printDiagnostics)
{
	m_PrintDiagnostics = printDiagnostics;
	m_SocketName = FString(socketName);
	m_IP = ip;
	m_Port = port;
	m_pCallback = callback;

	if (!StartTCPReceiver()) return;
}

void IdeaSisSocketWrapper::ShutDown()
{
	

	if (m_pConnectionSocket != NULL)
	{
		m_pConnectionSocket->Close();
		delete m_pConnectionSocket;
		m_pConnectionSocket = NULL;
	}

	if (m_pListenerSocket != NULL)
	{
		m_pListenerSocket->Close();
		delete m_pListenerSocket;
		m_pListenerSocket = NULL;
	}

	m_bStopping = true;
	m_pCallback = NULL;
}




IdeaSisSocketWrapper::~IdeaSisSocketWrapper()
{
	m_bStopping = true;

	ShutDown();
}

bool IdeaSisSocketWrapper::StartTCPReceiver()
{
	m_pListenerSocket = CreateTCPConnectionListener();

	if (!m_pListenerSocket)
	{
		UE_LOG(LogSocket, Error, TEXT("Listen socket could not be created: (%s %d)"), *m_IP, m_Port);

		return false;
	}

	UE_LOG(LogSocket, Display, TEXT("Listen socket is created: (%s %d)"), *m_IP, m_Port);

	m_pListenerThread = new thread(&IdeaSisSocketWrapper::ListenToConnection, this);

	return true;
}



// Create TCP Connection Listener
FSocket* IdeaSisSocketWrapper::CreateTCPConnectionListener()
{
	uint8 IP4Nums[4];
	if (!FormatIP4ToNumber(m_IP, IP4Nums))
	{
		return false;
	}

	FIPv4Endpoint Endpoint(FIPv4Address(IP4Nums[0], IP4Nums[1], IP4Nums[2], IP4Nums[3]), m_Port);
	FTcpSocketBuilder tcpSocketBuilder = FTcpSocketBuilder(*m_SocketName);
	tcpSocketBuilder = tcpSocketBuilder.AsReusable();
	tcpSocketBuilder = tcpSocketBuilder.BoundToEndpoint(Endpoint);
	FSocket* listenSocket = tcpSocketBuilder.Listening(8);

	if (listenSocket != NULL)
	{
		int32 NewSize = 0;
		listenSocket->SetReceiveBufferSize(m_BufferSize, NewSize);
	}

	return listenSocket;
}

// Listen to New Connection
void IdeaSisSocketWrapper::ListenToConnection()
{
	if (m_pListenerSocket == NULL) return;

	TSharedRef<FInternetAddr> RemoteAddress = ISocketSubsystem::Get(PLATFORM_SOCKETSUBSYSTEM)->CreateInternetAddr();
	bool Pending;

	// handle incoming connections
	while (!m_bStopping && m_pListenerSocket != NULL)
	{

		if (m_pListenerSocket != NULL && m_pListenerSocket->HasPendingConnection(Pending) && Pending)
		{
			//Already have a Connection? destroy previous
			if (m_pConnectionSocket)
			{
				m_pConnectionSocket->Close();
				ISocketSubsystem::Get(PLATFORM_SOCKETSUBSYSTEM)->DestroySocket(m_pConnectionSocket);
			}

			//New Connection receive!
			m_pConnectionSocket = m_pListenerSocket->Accept(*RemoteAddress, *m_SocketName);

			if (m_pConnectionSocket != NULL)
			{
				//Global cache of current Remote Address
				m_RemoteAddressForConnection = FIPv4Endpoint(RemoteAddress);

				m_pDataWaiter = new thread(&IdeaSisSocketWrapper::DataWaiter, this);
			}
		}
		
	}
}

void IdeaSisSocketWrapper::DataWaiter()
{
	while (!m_bStopping && m_pCallback != NULL)
	{
		m_pListenerData = new thread(&IdeaSisSocketWrapper::ListenToSocketData, this);
		m_pListenerData->join();
	}
}


//FString IdeaSisSocketWrapper::StringFromBinaryArray(const TArray<uint8>& BinaryArray)
//{
//	std::string cstr(reinterpret_cast<const char*>(BinaryArray.GetData()), BinaryArray.Num());
//	return FString(cstr.c_str());
//}

/// Listen to socket data
void IdeaSisSocketWrapper::ListenToSocketData()
{
	if (m_pConnectionSocket == nullptr) return;

	//Binary Array!
	TArray<uint8> ReceivedData;

	uint32 Size;
	while (!m_bStopping && m_pConnectionSocket != NULL && m_pConnectionSocket->HasPendingData(Size))
	{

		ReceivedData.SetNumUninitialized(FMath::Min(Size, 65507u));

		int32 Read = 0;
		m_pConnectionSocket->Recv(ReceivedData.GetData(), ReceivedData.Num(), Read);

		//GEngine->AddOnScreenDebugMessage(-1, 5.f, FColor::Red, FString::Printf(TEXT("Data Read! %d"), ReceivedData.Num()));

		for (size_t i = 0; i < (size_t)ReceivedData.Num(); i++)
		{
			FString ch = FString::Chr(ReceivedData[i]);
			if (ch == "\r")
			{
				UE_LOG(LogSocket, Display, TEXT("As String Data: %s"), *m_StringBuffer);

				if (!m_StringBuffer.IsEmpty())
				{
					m_pCallback->TriggerAction(*m_StringBuffer);
				}

				m_StringBuffer.Empty();
			}
			else if (ch == "\n")
			{
				return;
			}
			else
			{
				m_StringBuffer += ch;
			}
		}
	}
}

/// Format IP String as Number Parts
bool IdeaSisSocketWrapper::FormatIP4ToNumber(const FString& TheIP, uint8(&Out)[4])
{
	TheIP.Replace(TEXT(" "), TEXT(""));
	TArray<FString> Parts;
	TheIP.ParseIntoArray(Parts, TEXT("."), true);
	if (Parts.Num() != 4)
		return false;

	for (int32 i = 0; i < 4; ++i)
	{
		Out[i] = FCString::Atoi(*Parts[i]);
	}

	return true;
}