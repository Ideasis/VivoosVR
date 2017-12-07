// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "GeneratedCppIncludes.h"
#include "Private/IdeaSisSocketPlugInPrivatePCH.h"
#include "Public/IdeaSisSocketFunctionLibrary.h"
PRAGMA_DISABLE_OPTIMIZATION
#ifdef _MSC_VER
#pragma warning (push)
#pragma warning (disable : 4883)
#endif
PRAGMA_DISABLE_DEPRECATION_WARNINGS
void EmptyLinkFunctionForGeneratedCodeIdeaSisSocketFunctionLibrary() {}
// Cross Module References
	IDEASISSOCKETPLUGIN_API UFunction* Z_Construct_UDelegateFunction_IdeaSisSocketPlugIn_DataArrivedDelegate_DataArrived__DelegateSignature();
	UPackage* Z_Construct_UPackage__Script_IdeaSisSocketPlugIn();
	IDEASISSOCKETPLUGIN_API UFunction* Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_ShutDown();
	IDEASISSOCKETPLUGIN_API UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary();
	IDEASISSOCKETPLUGIN_API UFunction* Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_StartListening();
	IDEASISSOCKETPLUGIN_API UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary_NoRegister();
	ENGINE_API UClass* Z_Construct_UClass_UBlueprintFunctionLibrary();
// End Cross Module References
	UFunction* Z_Construct_UDelegateFunction_IdeaSisSocketPlugIn_DataArrivedDelegate_DataArrived__DelegateSignature()
	{
		struct _Script_IdeaSisSocketPlugIn_eventDataArrivedDelegate_DataArrived_Parms
		{
			FString Data;
		};
		UObject* Outer = Z_Construct_UPackage__Script_IdeaSisSocketPlugIn();
		static UFunction* ReturnFunction = nullptr;
		if (!ReturnFunction)
		{
			ReturnFunction = new(EC_InternalUseOnlyConstructor, Outer, TEXT("DataArrivedDelegate_DataArrived__DelegateSignature"), RF_Public|RF_Transient|RF_MarkAsNative) UDelegateFunction(FObjectInitializer(), nullptr, (EFunctionFlags)0x00130000, 65535, sizeof(_Script_IdeaSisSocketPlugIn_eventDataArrivedDelegate_DataArrived_Parms));
			UProperty* NewProp_Data = new(EC_InternalUseOnlyConstructor, ReturnFunction, TEXT("Data"), RF_Public|RF_Transient|RF_MarkAsNative) UStrProperty(CPP_PROPERTY_BASE(Data, _Script_IdeaSisSocketPlugIn_eventDataArrivedDelegate_DataArrived_Parms), 0x0010000000000080);
			ReturnFunction->Bind();
			ReturnFunction->StaticLink();
#if WITH_METADATA
			UMetaData* MetaData = ReturnFunction->GetOutermost()->GetMetaData();
			MetaData->SetValue(ReturnFunction, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
#endif
		}
		return ReturnFunction;
	}
	void UIdeaSisSocketFunctionLibrary::StaticRegisterNativesUIdeaSisSocketFunctionLibrary()
	{
		UClass* Class = UIdeaSisSocketFunctionLibrary::StaticClass();
		static const TNameNativePtrPair<ANSICHAR> AnsiFuncs[] = {
			{ "ShutDown", (Native)&UIdeaSisSocketFunctionLibrary::execShutDown },
			{ "StartListening", (Native)&UIdeaSisSocketFunctionLibrary::execStartListening },
		};
		FNativeFunctionRegistrar::RegisterFunctions(Class, AnsiFuncs, ARRAY_COUNT(AnsiFuncs));
	}
	UFunction* Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_ShutDown()
	{
		UObject* Outer = Z_Construct_UClass_UIdeaSisSocketFunctionLibrary();
		static UFunction* ReturnFunction = nullptr;
		if (!ReturnFunction)
		{
			ReturnFunction = new(EC_InternalUseOnlyConstructor, Outer, TEXT("ShutDown"), RF_Public|RF_Transient|RF_MarkAsNative) UFunction(FObjectInitializer(), nullptr, (EFunctionFlags)0x04020401, 65535);
			ReturnFunction->Bind();
			ReturnFunction->StaticLink();
#if WITH_METADATA
			UMetaData* MetaData = ReturnFunction->GetOutermost()->GetMetaData();
			MetaData->SetValue(ReturnFunction, TEXT("Category"), TEXT("IdeaSis"));
			MetaData->SetValue(ReturnFunction, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
#endif
		}
		return ReturnFunction;
	}
	UFunction* Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_StartListening()
	{
		UObject* Outer = Z_Construct_UClass_UIdeaSisSocketFunctionLibrary();
		static UFunction* ReturnFunction = nullptr;
		if (!ReturnFunction)
		{
			ReturnFunction = new(EC_InternalUseOnlyConstructor, Outer, TEXT("StartListening"), RF_Public|RF_Transient|RF_MarkAsNative) UFunction(FObjectInitializer(), nullptr, (EFunctionFlags)0x04020401, 65535);
			ReturnFunction->Bind();
			ReturnFunction->StaticLink();
#if WITH_METADATA
			UMetaData* MetaData = ReturnFunction->GetOutermost()->GetMetaData();
			MetaData->SetValue(ReturnFunction, TEXT("Category"), TEXT("IdeaSis"));
			MetaData->SetValue(ReturnFunction, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
#endif
		}
		return ReturnFunction;
	}
	UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary_NoRegister()
	{
		return UIdeaSisSocketFunctionLibrary::StaticClass();
	}
	UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary()
	{
		static UClass* OuterClass = NULL;
		if (!OuterClass)
		{
			Z_Construct_UClass_UBlueprintFunctionLibrary();
			Z_Construct_UPackage__Script_IdeaSisSocketPlugIn();
			OuterClass = UIdeaSisSocketFunctionLibrary::StaticClass();
			if (!(OuterClass->ClassFlags & CLASS_Constructed))
			{
				UObjectForceRegistration(OuterClass);
				OuterClass->ClassFlags |= (EClassFlags)0x20800080u;

				OuterClass->LinkChild(Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_ShutDown());
				OuterClass->LinkChild(Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_StartListening());

				UProperty* NewProp_DataRead = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("DataRead"), RF_Public|RF_Transient|RF_MarkAsNative) UStrProperty(CPP_PROPERTY_BASE(DataRead, UIdeaSisSocketFunctionLibrary), 0x0010000000000000);
				UProperty* NewProp_DataArrived = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("DataArrived"), RF_Public|RF_Transient|RF_MarkAsNative) UMulticastDelegateProperty(CPP_PROPERTY_BASE(DataArrived, UIdeaSisSocketFunctionLibrary), 0x0010100010080000, Z_Construct_UDelegateFunction_IdeaSisSocketPlugIn_DataArrivedDelegate_DataArrived__DelegateSignature());
				CPP_BOOL_PROPERTY_BITMASK_STRUCT(PrintToDiagnostics, UIdeaSisSocketFunctionLibrary);
				UProperty* NewProp_PrintToDiagnostics = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("PrintToDiagnostics"), RF_Public|RF_Transient|RF_MarkAsNative) UBoolProperty(FObjectInitializer(), EC_CppProperty, CPP_BOOL_PROPERTY_OFFSET(PrintToDiagnostics, UIdeaSisSocketFunctionLibrary), 0x0010000000000005, CPP_BOOL_PROPERTY_BITMASK(PrintToDiagnostics, UIdeaSisSocketFunctionLibrary), sizeof(bool), true);
				UProperty* NewProp_Alias = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("Alias"), RF_Public|RF_Transient|RF_MarkAsNative) UStrProperty(CPP_PROPERTY_BASE(Alias, UIdeaSisSocketFunctionLibrary), 0x0010000000000005);
				UProperty* NewProp_IPToListen = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("IPToListen"), RF_Public|RF_Transient|RF_MarkAsNative) UStrProperty(CPP_PROPERTY_BASE(IPToListen, UIdeaSisSocketFunctionLibrary), 0x0010000000000005);
				UProperty* NewProp_PortToListen = new(EC_InternalUseOnlyConstructor, OuterClass, TEXT("PortToListen"), RF_Public|RF_Transient|RF_MarkAsNative) UIntProperty(CPP_PROPERTY_BASE(PortToListen, UIdeaSisSocketFunctionLibrary), 0x0010000000000005);
				OuterClass->AddFunctionToFunctionMapWithOverriddenName(Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_ShutDown(), "ShutDown"); // 2563299628
				OuterClass->AddFunctionToFunctionMapWithOverriddenName(Z_Construct_UFunction_UIdeaSisSocketFunctionLibrary_StartListening(), "StartListening"); // 804079614
				OuterClass->ClassConfigName = FName(TEXT("Game"));
				static TCppClassTypeInfo<TCppClassTypeTraits<UIdeaSisSocketFunctionLibrary> > StaticCppClassTypeInfo;
				OuterClass->SetCppTypeInfo(&StaticCppClassTypeInfo);
				OuterClass->StaticLink();
#if WITH_METADATA
				UMetaData* MetaData = OuterClass->GetOutermost()->GetMetaData();
				MetaData->SetValue(OuterClass, TEXT("BlueprintSpawnableComponent"), TEXT(""));
				MetaData->SetValue(OuterClass, TEXT("BlueprintType"), TEXT("true"));
				MetaData->SetValue(OuterClass, TEXT("Category"), TEXT("IdeaSis"));
				MetaData->SetValue(OuterClass, TEXT("IncludePath"), TEXT("IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(OuterClass, TEXT("IsBlueprintBase"), TEXT("true"));
				MetaData->SetValue(OuterClass, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_DataRead, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_DataArrived, TEXT("Category"), TEXT("IdeaSis Events"));
				MetaData->SetValue(NewProp_DataArrived, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_PrintToDiagnostics, TEXT("Category"), TEXT("IdeaSis"));
				MetaData->SetValue(NewProp_PrintToDiagnostics, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_Alias, TEXT("Category"), TEXT("IdeaSis"));
				MetaData->SetValue(NewProp_Alias, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_IPToListen, TEXT("Category"), TEXT("IdeaSis"));
				MetaData->SetValue(NewProp_IPToListen, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
				MetaData->SetValue(NewProp_PortToListen, TEXT("Category"), TEXT("IdeaSis"));
				MetaData->SetValue(NewProp_PortToListen, TEXT("ModuleRelativePath"), TEXT("Public/IdeaSisSocketFunctionLibrary.h"));
#endif
			}
		}
		check(OuterClass->GetClass());
		return OuterClass;
	}
	IMPLEMENT_CLASS(UIdeaSisSocketFunctionLibrary, 329877189);
	static FCompiledInDefer Z_CompiledInDefer_UClass_UIdeaSisSocketFunctionLibrary(Z_Construct_UClass_UIdeaSisSocketFunctionLibrary, &UIdeaSisSocketFunctionLibrary::StaticClass, TEXT("/Script/IdeaSisSocketPlugIn"), TEXT("UIdeaSisSocketFunctionLibrary"), false, nullptr, nullptr, nullptr);
	DEFINE_VTABLE_PTR_HELPER_CTOR(UIdeaSisSocketFunctionLibrary);
PRAGMA_ENABLE_DEPRECATION_WARNINGS
#ifdef _MSC_VER
#pragma warning (pop)
#endif
PRAGMA_ENABLE_OPTIMIZATION
