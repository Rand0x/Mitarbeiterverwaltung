
function Append-Scripts
{
    $loc = $MyInvocation.ScriptName.TrimEnd("Append-Scripts.ps1")
    $go = $loc + "go.sql"

    $scripts = $loc + "scripts.sql"
    if ((Test-path $scripts) -eq $true) { Remove-Item $scripts }
    New-Item $scripts -ItemType file
    
    $directories = Get-ChildItem -Path $loc -Directory
    Get-Content $go | out-file $scripts -append
    foreach($dir in $directories)
    {
        $dir = $loc + $dir + "\"
        $files = Get-ChildItem -Path $dir -Filter "*.sql"
        foreach($file in $files)
        {
            $file = $dir + $file
            Get-Content $file | out-file $scripts -append
            Get-Content $go | out-file $scripts -append
        }
    }
}

Append-Scripts