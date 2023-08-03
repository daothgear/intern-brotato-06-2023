WORKSPACE="$PWD"
BUILD_FOLDER_PATH=${WORKSPACE}/Build/WebGl
INCLUDE_MINUTE_IN_BUILD_NAME=falSE

echo "working dir: $WORKSPACE"

/Applications/Unity/Hub/Editor/2021.3.19f1/Unity.app/Contents/MacOS/Unity \
	-projectPath ${WORKSPACE} \
	-executemethod BuildCommand.BuildWebGl \
	-inculeMinuteInBuildName ${INCLUDE_MINUTE_IN_BUILD_NAME} \
	-batchmode \
	-quit \
	-logfile ${BUILD_FOLDER_PATH}/buildunity.log \

echo "Done"
