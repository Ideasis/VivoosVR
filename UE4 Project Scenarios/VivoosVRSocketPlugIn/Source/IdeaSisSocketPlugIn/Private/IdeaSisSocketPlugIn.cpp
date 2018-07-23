// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.

#include "IdeaSisSocketPlugInPrivatePCH.h"


class FIdeaSisSocketPlugIn : public IIdeaSisSocketPlugIn
{
	/** IModuleInterface implementation */
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
};

IMPLEMENT_MODULE( FIdeaSisSocketPlugIn, IdeaSisSocketPlugIn )



void FIdeaSisSocketPlugIn::StartupModule()
{
	
}


void FIdeaSisSocketPlugIn::ShutdownModule()
{
	// This function may be called during shutdown to clean up your module.  For modules that support dynamic reloading,
	// we call this function before unloading the module.
}



