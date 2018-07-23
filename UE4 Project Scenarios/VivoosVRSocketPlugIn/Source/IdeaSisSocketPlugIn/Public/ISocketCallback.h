#pragma once



__interface ISocketCallback
{
	void TriggerAction(const FString & data);
	//FString GetSocketData();
};