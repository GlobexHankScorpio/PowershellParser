Param
( 
	[Parameter(Mandatory)] [string]$folderPath
)

New-Item -Path $folderPath -ItemType Directory