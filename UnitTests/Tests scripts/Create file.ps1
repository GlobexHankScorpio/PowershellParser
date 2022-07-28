Param
( 
	[Parameter(Mandatory)] [string]$filePath
)

New-Item -Path $filePath -ItemType File
