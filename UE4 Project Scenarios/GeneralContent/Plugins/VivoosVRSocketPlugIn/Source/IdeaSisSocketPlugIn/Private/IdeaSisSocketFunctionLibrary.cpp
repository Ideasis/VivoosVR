// Fill out your copyright notice in the Description page of Project Settings.

//#include "IdeaSisSocketFunctionLibrary.h"
#include "IdeaSisSocketPlugInPrivatePCH.h"

UIdeaSisSocketFunctionLibrary::UIdeaSisSocketFunctionLibrary(const FObjectInitializer& ObjectInitializer) : Super(ObjectInitializer), 
Alias(FString::Printf(TEXT("IdeaSis"))),
PortToListen(9001), 
IPToListen(FString::Printf(TEXT("127.0.0.1")))
{
	m_pSocket = new IdeaSisSocketWrapper();
}

void UIdeaSisSocketFunctionLibrary::StartListening()
{
	m_pSocket->StartListening(*Alias, *IPToListen, PortToListen, this, PrintToDiagnostics);
}

void UIdeaSisSocketFunctionLibrary::ShutDown()
{
	m_pSocket->ShutDown();
}