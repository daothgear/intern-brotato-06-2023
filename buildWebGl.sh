WORKSPACE="$PWD"
BUILD_FOLDER_PATH=${WORKSPACE}/Build/WebGl

echo "working dir: $WORKSPACE"

/Applications/Unity/Hub/Editor/2021.3.19f1/Unity.app/Contents/MacOS/Unity \
	-projectPath ${WORKSPACE} \
	-executemethod BuildCommand.BuildWebGl \
	-dataTestKey "dev_test" \
	-batchmode \
	-quit \
	-logfile ${BUILD_FOLDER_PATH}/buildunity.log \

echo "Done"
