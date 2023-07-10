#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#if UNITY_WEBGL && UNITY_EDITOR_OSX
public class WebglPreBuildProcessing : IPreprocessBuildWithReport {
    // source: http://answers.unity.com/comments/1894734/view.html
    // https://stackoverflow.com/a/71740144/9459898
    // https://forum.unity.com/threads/case-1412113-builderror-osx-12-3-and-unity-2020-3-constant-build-errors.1255419/
    // https://forum.unity.com/threads/system-componentmodel-win32exception-2-no-such-file-or-directory.1178101/#post-8063549
    // mac auto delete python 2 when upgrade to macOs Monterey 12.3.1
    // so we need to update EMSDK_PYTHON varible to python3
    public int callbackOrder => 1;

    public void OnPreprocessBuild(BuildReport report) {
        System.Environment.SetEnvironmentVariable("EMSDK_PYTHON", "/usr/local/bin/python3");
//        System.Environment.SetEnvironmentVariable("EMSDK_PYTHON", "/usr/local/bin/python2");
    }
}
#endif
#endif