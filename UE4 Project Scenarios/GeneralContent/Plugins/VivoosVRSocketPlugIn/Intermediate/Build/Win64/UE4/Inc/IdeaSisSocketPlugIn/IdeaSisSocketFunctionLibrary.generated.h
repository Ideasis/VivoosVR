// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.
/*===========================================================================
	Generated code exported from UnrealHeaderTool.
	DO NOT modify this manually! Edit the corresponding .h files instead!
===========================================================================*/

#include "ObjectMacros.h"
#include "ScriptMacros.h"

PRAGMA_DISABLE_DEPRECATION_WARNINGS
#ifdef IDEASISSOCKETPLUGIN_IdeaSisSocketFunctionLibrary_generated_h
#error "IdeaSisSocketFunctionLibrary.generated.h already included, missing '#pragma once' in IdeaSisSocketFunctionLibrary.h"
#endif
#define IDEASISSOCKETPLUGIN_IdeaSisSocketFunctionLibrary_generated_h

#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_7_DELEGATE \
struct _Script_IdeaSisSocketPlugIn_eventDataArrivedDelegate_DataArrived_Parms \
{ \
	FString Data; \
}; \
static inline void FDataArrivedDelegate_DataArrived_DelegateWrapper(const FMulticastScriptDelegate& DataArrivedDelegate_DataArrived, const FString& Data) \
{ \
	_Script_IdeaSisSocketPlugIn_eventDataArrivedDelegate_DataArrived_Parms Parms; \
	Parms.Data=Data; \
	DataArrivedDelegate_DataArrived.ProcessMulticastDelegate<UObject>(&Parms); \
}


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_RPC_WRAPPERS \
 \
	DECLARE_FUNCTION(execShutDown) \
	{ \
		P_FINISH; \
		P_NATIVE_BEGIN; \
		this->ShutDown(); \
		P_NATIVE_END; \
	} \
 \
	DECLARE_FUNCTION(execStartListening) \
	{ \
		P_FINISH; \
		P_NATIVE_BEGIN; \
		this->StartListening(); \
		P_NATIVE_END; \
	}


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_RPC_WRAPPERS_NO_PURE_DECLS \
 \
	DECLARE_FUNCTION(execShutDown) \
	{ \
		P_FINISH; \
		P_NATIVE_BEGIN; \
		this->ShutDown(); \
		P_NATIVE_END; \
	} \
 \
	DECLARE_FUNCTION(execStartListening) \
	{ \
		P_FINISH; \
		P_NATIVE_BEGIN; \
		this->StartListening(); \
		P_NATIVE_END; \
	}


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_INCLASS_NO_PURE_DECLS \
private: \
	static void StaticRegisterNativesUIdeaSisSocketFunctionLibrary(); \
	friend IDEASISSOCKETPLUGIN_API class UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary(); \
public: \
	DECLARE_CLASS(UIdeaSisSocketFunctionLibrary, UBlueprintFunctionLibrary, COMPILED_IN_FLAGS(0), 0, TEXT("/Script/IdeaSisSocketPlugIn"), NO_API) \
	DECLARE_SERIALIZER(UIdeaSisSocketFunctionLibrary) \
	enum {IsIntrinsic=COMPILED_IN_INTRINSIC}; \
	static const TCHAR* StaticConfigName() {return TEXT("Game");} \



#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_INCLASS \
private: \
	static void StaticRegisterNativesUIdeaSisSocketFunctionLibrary(); \
	friend IDEASISSOCKETPLUGIN_API class UClass* Z_Construct_UClass_UIdeaSisSocketFunctionLibrary(); \
public: \
	DECLARE_CLASS(UIdeaSisSocketFunctionLibrary, UBlueprintFunctionLibrary, COMPILED_IN_FLAGS(0), 0, TEXT("/Script/IdeaSisSocketPlugIn"), NO_API) \
	DECLARE_SERIALIZER(UIdeaSisSocketFunctionLibrary) \
	enum {IsIntrinsic=COMPILED_IN_INTRINSIC}; \
	static const TCHAR* StaticConfigName() {return TEXT("Game");} \



#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_STANDARD_CONSTRUCTORS \
	/** Standard constructor, called after all reflected properties have been initialized */ \
	NO_API UIdeaSisSocketFunctionLibrary(const FObjectInitializer& ObjectInitializer = FObjectInitializer::Get()); \
	DEFINE_DEFAULT_OBJECT_INITIALIZER_CONSTRUCTOR_CALL(UIdeaSisSocketFunctionLibrary) \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, UIdeaSisSocketFunctionLibrary); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(UIdeaSisSocketFunctionLibrary); \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API UIdeaSisSocketFunctionLibrary(UIdeaSisSocketFunctionLibrary&&); \
	NO_API UIdeaSisSocketFunctionLibrary(const UIdeaSisSocketFunctionLibrary&); \
public:


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_ENHANCED_CONSTRUCTORS \
	/** Standard constructor, called after all reflected properties have been initialized */ \
	NO_API UIdeaSisSocketFunctionLibrary(const FObjectInitializer& ObjectInitializer = FObjectInitializer::Get()) : Super(ObjectInitializer) { }; \
private: \
	/** Private move- and copy-constructors, should never be used */ \
	NO_API UIdeaSisSocketFunctionLibrary(UIdeaSisSocketFunctionLibrary&&); \
	NO_API UIdeaSisSocketFunctionLibrary(const UIdeaSisSocketFunctionLibrary&); \
public: \
	DECLARE_VTABLE_PTR_HELPER_CTOR(NO_API, UIdeaSisSocketFunctionLibrary); \
DEFINE_VTABLE_PTR_HELPER_CTOR_CALLER(UIdeaSisSocketFunctionLibrary); \
	DEFINE_DEFAULT_OBJECT_INITIALIZER_CONSTRUCTOR_CALL(UIdeaSisSocketFunctionLibrary)


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_PRIVATE_PROPERTY_OFFSET
#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_9_PROLOG
#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_GENERATED_BODY_LEGACY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_PRIVATE_PROPERTY_OFFSET \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_RPC_WRAPPERS \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_INCLASS \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_STANDARD_CONSTRUCTORS \
public: \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


#define Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_GENERATED_BODY \
PRAGMA_DISABLE_DEPRECATION_WARNINGS \
public: \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_PRIVATE_PROPERTY_OFFSET \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_RPC_WRAPPERS_NO_PURE_DECLS \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_INCLASS_NO_PURE_DECLS \
	Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h_12_ENHANCED_CONSTRUCTORS \
static_assert(false, "Unknown access specifier for GENERATED_BODY() macro in class IdeaSisSocketFunctionLibrary."); \
PRAGMA_ENABLE_DEPRECATION_WARNINGS


#undef CURRENT_FILE_ID
#define CURRENT_FILE_ID Yukseklik_Plugins_IdeaSisSocketPlugIn_Source_IdeaSisSocketPlugIn_Public_IdeaSisSocketFunctionLibrary_h


PRAGMA_ENABLE_DEPRECATION_WARNINGS
