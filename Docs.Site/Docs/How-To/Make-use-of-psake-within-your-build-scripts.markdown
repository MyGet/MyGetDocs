# Make use of psake within your build scripts

Depending on the complexity of your project, you may have to script numerous build steps.  This might include using MSBuild, PowerShell, rake, bake, psake, or your own custom scripts.  What you use is really a personal preference, however, MyGet makes it especially easy to make use of psake.

## What is psake?

>psake is a build automation tool written in PowerShell. It avoids the angle-bracket tax associated with executable XML by leveraging the PowerShell syntax in your build scripts. psake has a syntax inspired by rake (aka make in Ruby) and bake (aka make in Boo), but is easier to script because it leverages your existent command-line knowledge.

For more information be sure to check out the [psake GitHub page](https://github.com/psake/psake "psake GitHub Page").

## Use local psake package

This is really simple!

The MyGet Build Server already has psake installed.  As a result, you can directly call _psake_ in your build scripts.  As an example, let's assume that you are using a PowerShell script as part of your build.  There is no requirement to load the psake Module, as there is already a psake.bat file located on the Path environment variable which takes care of all that for you.  As a result, all you need to do is the following::

    $here = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

    psake "$here/default.ps1" -Task BuildSolution;

Notice that the call to psake has not been qualified in any way, but it works quite happily.

## Use psake from within source code

Although you _can_ make use of the local psake scripts that are available on the MyGet Build Server, this may not work for you.  For instance, you may need to target a specific version of psake, which the MyGet Build Servers may not have installed.  In this case, one option that you would have would be to include the necessary files within your Source Control Repository, and call the psake scripts directly from there.  This is possible due to the fact that the MyGet Build Server downloads all the source code from your repository before executing the build.

This is typically done by creating a __lib__ or a __sharedbinaries__ folder in your Source Control Repository, and include the psake files there, for example:

![Include psake files within Source Control](Images/psake_include_in_source_control.png)


Once in place, you can then invoke the psake module by first locating it in your source tree, and then running it, for example:

    $here = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)";
    $psakePath = Join-Path $here -Child "lib\psake\psake.psm1";
    Import-Module $psakePath;

    invoke-psake "$here/default.ps1" -Task BuildSolution -Properties @{ 'config'=$Config; };