// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.

#include "IIdeaSisSocketPlugIn.h"

// You should place include statements to your module's private header files here.  You only need to
// add includes for headers that are used in most of your module's source files though.

#pragma once

#include "AssertionMacros.h"

#include "Networking.h"
#include "Sockets.h"
#include "thread"
#include "queue"

#include "Engine.h"

#include "Kismet/BlueprintFunctionLibrary.h"
#include "IdeaSisSocketWrapper.h"
#include "IdeaSisSocketFunctionLibrary.h"



DEFINE_LOG_CATEGORY_STATIC(LogSocket, Log, All);
