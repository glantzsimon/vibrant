param([String]$publishPassword='')

$publishDir = "publish"
$appDir = "webapp"
$projectPath = ".\WebApplication\WebApplication.csproj"
$webTestFile = ".\webapp\WebApplication.Tests\bin\Debug\K9.WebApplication.Tests.dll"
$dataTestFile = ".\webapp\DataAccess.Tests\bin\Debug\K9.DataAccess.Tests.dll"
	
function ProcessErrors(){
  if($? -eq $false)
  {
    throw "The previous command failed (see above)";
  }
}

function _DeleteFile($fileName) {
  If (Test-Path $fileName) {
    Write-Host "Deleting '$fileName'"
    Remove-Item $fileName
  } else {
    "'$fileName' not found. Nothing deleted"
  }
}

function _CreateDirectory($dir) {
  If (-Not (Test-Path $dir)) {
    New-Item -ItemType Directory -Path $dir
  }
}

function _NugetRestore() {
  echo "Running nuget restore"

  pushd $appDir
  ProcessErrors
  
  nuget restore
  ProcessErrors
  popd
}

function _Test() {
  echo "Running dotnet test"
  
  pushd $appDir  
  ProcessErrors
  
  "packages\xunit.runner.console.2.2.0\tools\xunit.console.exe " + $webTestFile
  ProcessErrors
  
  "packages\xunit.runner.console.2.2.0\tools\xunit.console.exe " + $dataTestFile
  ProcessErrors
  popd
}

function _Build() {
  echo "Building App"
  
  pushd $appDir
  ProcessErrors
  
  Msbuild /p:Configuration=Debug
  ProcessErrors
  popd
}

function _Publish() {
  echo "Publishing App"
  
  pushd $appDir
  ProcessErrors
  
  _CreateDirectory $publishDir
  ProcessErrors
  
  Msbuild $projectPath /p:DeployOnBuild=true /p:PublishProfile=Integration /p:AllowUntrustedCertificate=true /p:Password=$publishPassword
  ProcessErrors
  popd
}

function Main {
  Try {
    _Build
	_Test
	_Publish
  }
  Catch {
    Write-Error $_.Exception
    exit 1
  }
}

Main