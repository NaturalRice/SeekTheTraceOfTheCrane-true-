using System.IO;
using UnityEngine;

public class SceneExporter : MonoBehaviour
{
    // 该脚本用于导出场景信息，方便跟通义灵码进行任务对接
    private void Start()
    {
        // 使用相对路径，确保在不同环境中都能正常工作
        string filePath = "Assets/SceneExport.txt";
        ExportSceneToFile(filePath);
    }

    private void ExportSceneToFile(string filePath)
    {
        // 确保文件路径不为空
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("文件路径不能为空");
            return;
        }

        // 获取文件的目录路径
        string directory = Path.GetDirectoryName(filePath);

        // 如果目录不存在，则创建目录
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 使用 StreamWriter 将场景信息写入文件
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // 遍历所有游戏对象
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                writer.WriteLine($"游戏对象: {obj.name}");
                writer.WriteLine("- 组件:");

                // 遍历游戏对象的所有组件
                foreach (Component component in obj.GetComponents<Component>())
                {
                    if (component != null)
                    {
                        writer.WriteLine($"  - {component.GetType().Name}");
                    }
                }

                // 遍历子对象
                foreach (Transform child in obj.transform)
                {
                    writer.WriteLine($"子对象: {child.name}");
                    writer.WriteLine("  - 组件:");

                    // 遍历子对象的所有组件
                    foreach (Component component in child.GetComponents<Component>())
                    {
                        if (component != null)
                        {
                            writer.WriteLine($"    - {component.GetType().Name}");
                        }
                    }
                }

                writer.WriteLine();
            }
        }

        Debug.Log($"场景信息已导出到 {filePath}");
    }
}
