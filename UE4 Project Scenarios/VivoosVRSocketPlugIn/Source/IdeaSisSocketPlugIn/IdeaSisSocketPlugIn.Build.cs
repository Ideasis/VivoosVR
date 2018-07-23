// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.

namespace UnrealBuildTool.Rules
{
	public class IdeaSisSocketPlugIn : ModuleRules
	{
		public IdeaSisSocketPlugIn(TargetInfo Target)
		{
			PublicIncludePaths.AddRange(
				new string[] {
                    "Developer/IdeaSisSocketPlugIn/Public"
					// ... add public include paths required here ...
				}
				);

			PrivateIncludePaths.AddRange(
				new string[] {
					"Developer/IdeaSisSocketPlugIn/Private",
					// ... add other private include paths required here ...
				}
				);

			PublicDependencyModuleNames.AddRange(
				new string[]
				{
                    "Core", "CoreUObject", "Engine", "InputCore", "Sockets", "Networking"
					// ... add other public dependencies that you statically link with here ...
				}
				);

			PrivateDependencyModuleNames.AddRange(
				new string[]
				{
					// ... add private dependencies that you statically link with here ...
				}
				);

			DynamicallyLoadedModuleNames.AddRange(
				new string[]
				{
					// ... add any modules that your module loads dynamically here ...
				}
				);
		}
	}
}