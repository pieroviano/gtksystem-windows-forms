## һ�����Ӵ����Դ�ļ���Resources
��Ŀ¼���Ǳ���ģ�ֻ����ʹ���������Դ�ſ�����Ҫ�������뿴����ġ�ȫ�ֹ�����ԴProperties/Resources.resx���͡������ռ��ԴForm.resx����ʹ�÷�����
һ������£�����ͼƬ��Դ����ֱ�Ӽ���ԭ��ʹ�ã���������������ã�ͼƬ����Դ������ͼƬ��ŵ�Resources�ļ����¡�

#### �����������£�
����Ŀ�ºͱ������Ŀ¼�´���Resources�ļ��У���Resources��Դ��ŵ�ͼƬ��Form.resx�ļ����Ƶ�Resources�ļ��У����ļ��к��ļ�ȫ�����ɵ�������Ŀ�������Ŀ¼�¡�


## ����ʹ��ȫ�ֹ�����ԴProperties/Resources.resx
### �½�System.Resources.ResourceManager��<br/>
����Ŀ���½�System.Resources.ResourceManager�࣬�̳�GTKSystem.Resources.ResourceManager�����ڸ���ԭ��System.Resources.ResourceManager�ࡣ
GTKSystem.Resources.ResourceManagerʵ������Ŀ��Դ�ļ���ͼ���ļ���ȡ��
�����Ŀ��û��ʹ����Դͼ���ļ������Բ����½����ļ���

### �½�System.ComponentModel.ComponentResourceManager��<br/>
����Ŀ���½�System.ComponentModel.ComponentResourceManager�࣬�̳�GTKSystem.ComponentModel.ComponentResourceManager�����ڸ���ԭ��System.ComponentModel.ComponentResourceManager�ࡣ<br/>
GTKSystem.ComponentModel.ComponentResourceManagerʵ������Ŀ��Դ�ļ���ͼ���ļ���ȡ������GTKSystem.Resources.ResourceManager����
�����Ŀ��û��ʹ����Դͼ���ļ������Բ����½����ļ���

## ����ʹ�ô����ռ��ԴForm.resx
### �½�System.ComponentModel.ComponentResourceManager��<br/>
����Ŀ���½�System.ComponentModel.ComponentResourceManager�࣬�̳�GTKSystem.ComponentModel.ComponentResourceManager�����ڸ���ԭ��System.ComponentModel.ComponentResourceManager�ࡣ<br/>
GTKSystem.ComponentModel.ComponentResourceManagerʵ������Ŀ��Դ�ļ���ͼ���ļ���ȡ������GTKSystem.Resources.ResourceManager����
�����Ŀ��û��ʹ����Դͼ���ļ������Բ����½����ļ���

### ͼƬ����Դ��ʹ��
����GTKSystem�޷���ȡͼƬ��(ImageList)����Ҫ��ͼƬ���ͼƬ���뵽��Ŀ��Resources�ļ����£��磺
```
Form2.Designer.cs�����ó������£�
 imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
 imageList1.TransparentColor = System.Drawing.ColorExtension.Transparent;
 imageList1.Images.SetKeyName(0, "010.jpg");
 imageList1.Images.SetKeyName(1, "timg2.jpg");

��ô��Ҫ��ͼƬ010.jpg��timg2.jpg���Ƶ��ļ���Resources��Resources/imageList1��
```


