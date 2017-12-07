// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "IdeaSisSocketFunctionLibrary.generated.h"

DECLARE_DYNAMIC_MULTICAST_DELEGATE_OneParam(FDataArrivedDelegate_DataArrived, FString, Data);

UCLASS(Blueprintable, BlueprintType, meta = (BlueprintSpawnableComponent), Category = "IdeaSis", Config=Game)
class UIdeaSisSocketFunctionLibrary : public UBlueprintFunctionLibrary, public ISocketCallback
{
	GENERATED_UCLASS_BODY()
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "IdeaSis")
		int32 PortToListen;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "IdeaSis")
		FString IPToListen;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "IdeaSis")
		FString Alias;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "IdeaSis")
		bool PrintToDiagnostics = false;
	
public:
	UPROPERTY(BlueprintAssignable, BlueprintCallable, Category = "IdeaSis Events")
	FDataArrivedDelegate_DataArrived DataArrived;
	
	IdeaSisSocketWrapper* m_pSocket;

	~UIdeaSisSocketFunctionLibrary()
	{
		if (m_pSocket != nullptr)
		{
			
			delete m_pSocket;
			m_pSocket = nullptr;
		}
	}

	UFUNCTION(BlueprintCallable, Category = "IdeaSis")
	void StartListening();

	UFUNCTION(BlueprintCallable, Category = "IdeaSis")
	void ShutDown();

	UPROPERTY()
	FString DataRead;

	void TriggerAction(const FString & data)
	{
		DataRead = data;
		DataArrived.Broadcast(data);
	}
};
