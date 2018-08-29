$TAG = (git describe --tags --long  --match "v/*").split('/')[1]

Write-Host $TAG
$MAJOR = $TAG.split('.')[0]
$MINOR = $TAG.split('.')[1].split('-')[0]
$PATCH = $TAG.split('.')[2].split('-')[0]
$TICK = $TAG.split('.')[2].split('-')[1]
$HASH = $TAG.split('.')[2].split('-')[2]

$env:SOURCE_TAG=$TAG
$env:SOURCE_VERSION=$MAJOR.$MINOR.$PATCH.$TICK
$env:SOURCE_HASH=$HASH