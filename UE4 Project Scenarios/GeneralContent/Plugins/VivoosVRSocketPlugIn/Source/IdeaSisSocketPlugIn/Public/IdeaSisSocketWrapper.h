// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ISocketCallback.h"



using namespace std;



class IdeaSisSocketWrapper
{
private:
	// Members
	thread* m_pListenerThread;
	thread* m_pListenerData;
	thread* m_pDataWaiter;

	FString m_StringBuffer;
	FSocket* m_pListenerSocket;
	FSocket* m_pConnectionSocket;
	FIPv4Endpoint m_RemoteAddressForConnection;
	bool m_PrintDiagnostics;
	FString m_SocketName;
	FString m_IP;
	int32 m_Port;
	int32 m_BufferSize = 2 * 1024 * 1024;
	ISocketCallback* m_pCallback;

	// Methods
	bool StartTCPReceiver();
	FSocket* CreateTCPConnectionListener();
	void ListenToConnection();
	void ListenToSocketData();
	void DataWaiter();

	bool m_bStopping = false;

	static bool FormatIP4ToNumber(const FString& ip, uint8(&Out)[4]);

public:
	// Starter
	void StartListening(const TCHAR* socketName, const TCHAR* ip, const int32 port, ISocketCallback* callback, const int32 bufferSize = 2 * 1024 * 1024, bool printDiagnostics = false);
	void ShutDown();

	~IdeaSisSocketWrapper();
};

