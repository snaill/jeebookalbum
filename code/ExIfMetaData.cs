using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;

namespace Tool.Image
{
	/// <summary>
	/// ���ܣ����ͼƬExif��Ϣ
	/// http://blog.ppcode.com
	/// </summary>
	public class EXIFMetaData
	{
		#region ���캯��
		/// <summary>
		/// ��������
		/// </summary>
		public EXIFMetaData()
		{
		}
		#endregion

		#region Ԫ���ݽṹ��
		/// <summary>
		/// Ԫ���ݽṹ��
		/// </summary>
		public struct MetadataDetail
		{
			public string Hex;
			public string RawValueAsString;
			public string DisplayValue;
		}
		#endregion

		#region JPG��Ƭ��Meta���ݽṹ
		/// <summary>
		/// JPG��Ƭ��Meta���ݽṹ
		/// </summary>
		public struct Metadata
		{
			public MetadataDetail EquipmentMake;
			public MetadataDetail CameraModel;
			public MetadataDetail ExposureTime;//�ع�ʱ��
			public MetadataDetail Fstop;
			public MetadataDetail DatePictureTaken;
			public MetadataDetail ShutterSpeed;// �����ٶ�
			public MetadataDetail MeteringMode;//�ع�ģʽ
			public MetadataDetail Flash;//�����
			public MetadataDetail XResolution;
			public MetadataDetail YResolution;
			public MetadataDetail ImageWidth;//��Ƭ���
			public MetadataDetail ImageHeight;//��Ƭ�߶�

			public MetadataDetail FNumber;// added fֵ����Ȧֵ
			public MetadataDetail ExposureProg;// added �ع����
			public MetadataDetail SpectralSense;// added 
			public MetadataDetail ISOSpeed;// added ISO�й��
			public MetadataDetail OECF;// added 
			public MetadataDetail Ver;// added Exif�汾
			public MetadataDetail CompConfig;// added ɫ������
			public MetadataDetail CompBPP;// added ѹ������
			public MetadataDetail Aperture;// added ��Ȧֵ
			public MetadataDetail Brightness;// added ����ֵEv
			public MetadataDetail ExposureBias;// added �عⲹ��
			public MetadataDetail MaxAperture;// added ����Ȧֵ

			public MetadataDetail SubjectDist;// added�������
			public MetadataDetail LightSource;// added ��ƽ��
			public MetadataDetail FocalLength;// added ����
			public MetadataDetail FPXVer;// added FlashPix�汾
			public MetadataDetail ColorSpace;// added ɫ�ʿռ�
			public MetadataDetail Interop;// added 
			public MetadataDetail FlashEnergy;// added 
			public MetadataDetail SpatialFR;// added 
			public MetadataDetail FocalXRes;// added 
			public MetadataDetail FocalYRes;// added 
			public MetadataDetail FocalResUnit;// added 
			public MetadataDetail ExposureIndex;// added �ع�ָ��
			public MetadataDetail SensingMethod;// added ��Ӧ��ʽ
			public MetadataDetail SceneType;// added 
			public MetadataDetail CfaPattern;// added 
		}
		#endregion

		#region ��Ԫ����ת���ɶ�Ӧ��ֵ��Ϣ
		/// <summary>
		/// ��Ԫ����ת���ɶ�Ӧ��ֵ��Ϣ
		/// </summary>
		/// <param name="Description">��������</param>
		/// <param name="Value">���Ͷ�Ӧ��Ԫ����ֵ</param>
		/// <returns>Ԫ��������Ӧ��EXIF��Ϣ</returns>
		public string LookupEXIFValue(string Description, string Value)
		{
			string DescriptionValue = null;
			switch(Description)
			{
				case "MeteringMode":
					#region ���ģʽ
				switch(Value)
				{
					case "0":
						DescriptionValue = "Unknown";break;
					case "1":
						DescriptionValue = "Average";break;
					case "2":
						DescriptionValue = "Center Weighted Average";break;
					case "3":
						DescriptionValue = "Spot";break;
					case "4":
						DescriptionValue = "Multi-spot";break;
					case "5":
						DescriptionValue = "Multi-segment";break;
					case "6":
						DescriptionValue = "Partial";break;
					case "255":
						DescriptionValue = "Other";break;
				}
					#endregion
					break;
				case "ResolutionUnit":
					#region �ֱ��ʵ�λ
				switch(Value)
				{
					case "1":
						DescriptionValue = "No Units";break;
					case "2":
						DescriptionValue = "Inch";break;
					case "3":
						DescriptionValue = "Centimeter";break;
				}
					#endregion
					break;
				case "Flash":
					#region �����
				switch(Value)
				{
					case "0":
						DescriptionValue = "δʹ��";break;//Flash did not fire
					case "1":
						DescriptionValue = "����";break;//Flash fired
					case "5":
						DescriptionValue = "Flash fired but strobe return light not detected";break;
					case "7":
						DescriptionValue = "Flash fired and strobe return light detected";break;
				}
					#endregion
					break;
				case "ExposureProg":
					#region �ع�ģʽ
				switch(Value)
				{
					case "0":
						DescriptionValue = "û�ж���";break;//Not defined
					case "1":
						DescriptionValue = "�ֶ�����";break;//Manual
					case "2":
						DescriptionValue = "�������";break;//Normal program
					case "3":
						DescriptionValue = "��Ȧ����";break;//Aperture priority
					case "4":
						DescriptionValue = "��������";break;//Shutter priority
					case "5":
						DescriptionValue = "ҹ��ģʽ";break;//Creative program (biased toward depth of field)
					case "6":
						DescriptionValue = "�˶�ģʽ";break;//Action program (biased toward fast shutter speed)
					case "7":
						DescriptionValue = "Ф��ģʽ";break;//Portrait mode (for closeup photos with the background out of focus)
					case "8":
						DescriptionValue = "�羰ģʽ";break;//Landscape mode (for landscape photos with the background in focus)
					case "9":
						DescriptionValue = "������";break;//Reserved
				}
					#endregion
					break;
				case "LightSource":
					#region ��Դ
				switch(Value)
				{
					case "0":
						DescriptionValue = "δ֪";break;//Unknown
					case "1":
						DescriptionValue = "�չ�";break;//Day Light
					case "2":
						DescriptionValue = "ӫ���";break;//Fluorescent
					case "3":
						DescriptionValue = "�׳��";break;//Tungsten
					case "10":
						DescriptionValue = "�����";break;//Flash
					case "17":
						DescriptionValue = "��׼��A";break;//Standard Light A
					case "18":
						DescriptionValue = "��׼��B";break;//Standard Light B
					case "19":
						DescriptionValue = "��׼��C";break;//Standard Light C
					case "20":
						DescriptionValue = "��׼��D55";break;//Standard Light D55
					case "21":
						DescriptionValue = "��׼��D65";break;//Standard Light D65
					case "22":
						DescriptionValue = "��׼��D75";break;//Standard Light D75
					case "255":
						DescriptionValue = "����";break;//Other
				}
					#endregion
					break;
				case "CompConfig":
				switch(Value)
				{
							
					case "513":
						DescriptionValue = "YCbCr";break;
				}
					break;

				case "Aperture":
					DescriptionValue = Value;
					break;

			}
			return DescriptionValue;
		}
		#endregion

		#region ȡ��ͼƬ��EXIF��Ϣ#region ȡ��ͼƬ��EXIF��Ϣ
		public Metadata GetEXIFMetaData(string PhotoName)
		{
			// ����һ��ͼƬ��ʵ��
			System.Drawing.Image MyImage = System.Drawing.Image.FromFile(PhotoName);
			// ����һ�������������洢ͼ�������������ID 
			int[] MyPropertyIdList = MyImage.PropertyIdList;
			//����һ�����ͼ�����������ʵ��
			PropertyItem[] MyPropertyItemList = new PropertyItem[MyPropertyIdList.Length];
			//����һ��ͼ��EXIT��Ϣ��ʵ���ṹ���󣬲��Ҹ���ֵ
			#region ����һ��ͼ��EXIT��Ϣ��ʵ���ṹ���󣬲��Ҹ���ֵ
			Metadata MyMetadata = new Metadata();
			MyMetadata.EquipmentMake.Hex = "10f";
			MyMetadata.CameraModel.Hex = "110";
			MyMetadata.DatePictureTaken.Hex	= "9003";
			MyMetadata.ExposureTime.Hex	= "829a";
			MyMetadata.Fstop.Hex = "829d";
			MyMetadata.ShutterSpeed.Hex = "9201";
			MyMetadata.MeteringMode.Hex = "9207";
			MyMetadata.Flash.Hex = "9209";
			MyMetadata.FNumber.Hex = "829d"; //added 
			MyMetadata.ExposureProg.Hex = ""; //added 
			MyMetadata.SpectralSense.Hex = "8824"; //added 
			MyMetadata.ISOSpeed.Hex = "8827"; //added 
			MyMetadata.OECF.Hex = "8828"; //added 
			MyMetadata.Ver.Hex = "9000"; //added 
			MyMetadata.CompConfig.Hex = "9101"; //added 
			MyMetadata.CompBPP.Hex = "9102"; //added 
			MyMetadata.Aperture.Hex = "9202"; //added 
			MyMetadata.Brightness.Hex = "9203"; //added 
			MyMetadata.ExposureBias.Hex = "9204"; //added 
			MyMetadata.MaxAperture.Hex = "9205"; //added 
			MyMetadata.SubjectDist.Hex = "9206"; //added 
			MyMetadata.LightSource.Hex = "9208"; //added 
			MyMetadata.FocalLength.Hex = "920a"; //added 
			MyMetadata.FPXVer.Hex = "a000"; //added 
			MyMetadata.ColorSpace.Hex = "a001"; //added 
			MyMetadata.FocalXRes.Hex = "a20e"; //added 
			MyMetadata.FocalYRes.Hex = "a20f"; //added 
			MyMetadata.FocalResUnit.Hex = "a210"; //added 
			MyMetadata.ExposureIndex.Hex = "a215"; //added 
			MyMetadata.SensingMethod.Hex = "a217"; //added 
			MyMetadata.SceneType.Hex = "a301";
			MyMetadata.CfaPattern.Hex = "a302";
			#endregion

			// ASCII���� 
			System.Text.ASCIIEncoding Value = new System.Text.ASCIIEncoding();
			int index = 0;
			#region ȡ������
			int MyPropertyIdListCount=MyPropertyIdList.Length; 
			if(MyPropertyIdListCount!=0) 
			{ 
				foreach (int MyPropertyId in MyPropertyIdList) 
				{ 
					string hexVal = ""; 
					MyPropertyItemList[index] = MyImage.GetPropertyItem(MyPropertyId); 

					byte[] eixfValueArr = MyImage.GetPropertyItem(MyPropertyId).Value; //Updated by Anders Hu 2004/10/10
					string itemValue = BitConverter.ToString(eixfValueArr);//Tag Valueֵ��16���Ʊ�ʾ(ʹ�ã�����)
					string[] longValueArr = itemValue.Split('-');


 
					#region ��ʼ��������ֵ 
					string myPropertyIdString=MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x"); 
					switch(myPropertyIdString) 
					{ 
						case "10f": 
						{ 
							MyMetadata.EquipmentMake.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem (MyPropertyId).Value); 
							MyMetadata.EquipmentMake.DisplayValue = Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
 
						case "110": 
						{ 
							MyMetadata.CameraModel.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.CameraModel.DisplayValue =Value.GetString(MyPropertyItemList[index].Value); 
							break; 
 
						} 
 
						case "9003": 
						{ 
							MyMetadata.DatePictureTaken.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.DatePictureTaken.DisplayValue =Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
 
						case "9207": 
						{ 
							MyMetadata.MeteringMode.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.MeteringMode.DisplayValue = LookupEXIFValue("MeteringMode",BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString()); 
							break; 
						} 
 
						case "9209": 
						{ 
							MyMetadata.Flash.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.Flash.DisplayValue = LookupEXIFValue("Flash",BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString()); 
							break; 
						} 
 
						case "829a": 
						{ 
							MyMetadata.ExposureTime.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							string StringValue = ""; 
							for (int Offset = 0; Offset < MyImage.GetPropertyItem (MyPropertyId).Len; Offset = Offset + 4) 
							{ 
								StringValue += BitConverter.ToInt32(MyImage.GetPropertyItem(MyPropertyId).Value,Offset).ToString() + "/"; 
							} 
							MyMetadata.ExposureTime.DisplayValue =StringValue.Substring(0,StringValue.Length-1); 
							break; 
						} 
						case "829d": 
						{ 
							MyMetadata.Fstop.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							string[] tempValueArr;
							tempValueArr = new String[4]{longValueArr[0] , longValueArr[1],longValueArr[2],longValueArr[3]};
							double exifValueLow = EXIFMetaData.GetLongValue(tempValueArr);
							tempValueArr = new String[4]{longValueArr[4] , longValueArr[5],longValueArr[6],longValueArr[7]};
							double exifValueHigth = EXIFMetaData.GetLongValue(tempValueArr);
							double exifValueRational = EXIFMetaData.GetRationalValue(exifValueLow,exifValueHigth);
							MyMetadata.FNumber.DisplayValue = "F " + exifValueRational.ToString() ;


							break; 
						} 
						case "9201": 
						{ 

							MyMetadata.ShutterSpeed.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
							string StringValue = BitConverter.ToInt32(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString();
							MyMetadata.ShutterSpeed.DisplayValue = "1/" + StringValue + "s";

							break; 
						} 
 
						case "8822": 
						{ 
							MyMetadata.ExposureProg.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.ExposureProg.DisplayValue = LookupEXIFValue("ExposureProg",BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString()); 
							break; 
						} 
 
						case "8824": 
						{ 
							MyMetadata.SpectralSense.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.SpectralSense.DisplayValue =Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
						case "8827": 
						{ 
							hexVal = ""; 
							MyMetadata.ISOSpeed.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 

							int length = longValueArr.Length;
						
							double longValue = 0.0;
							for(int i = 0 ; i < length ; ++i)
							{
								longValue += Double.Parse(Convert.ToInt64(longValueArr[i],16).ToString("d")) * Math.Pow(16,i*2);
							}

							MyMetadata.ISOSpeed.DisplayValue = longValue.ToString();

							break; 
						} 
 
						case "8828": 
						{ 
							MyMetadata.OECF.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.OECF.DisplayValue =Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
 
						case "9000": 
						{ 
							MyMetadata.Ver.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.Ver.DisplayValue =Value.GetString(MyPropertyItemList[index].Value).Substring(1,1)+"."+Value.GetString(MyPropertyItemList[index].Value).Substring(2,2); 
							break; 
						} 
 
						case "9101": 
						{ 
							MyMetadata.CompConfig.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.CompConfig.DisplayValue = LookupEXIFValue("CompConfig",BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString()); 
							break; 
						} 
 
						case "9102": 
						{ 
							MyMetadata.CompBPP.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.CompBPP.DisplayValue = BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString(); 
							break; 
						} 
 
						case "9202": 
						{ 
							hexVal = "";
							MyMetadata.Aperture.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
							hexVal = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0,2);
							hexVal = Convert.ToInt32(hexVal,16).ToString();
							hexVal = hexVal + "00";
							MyMetadata.Aperture.DisplayValue = hexVal.Substring(0,1)+"."+hexVal.Substring(1,2);
							break; 
						} 
 
						case "9203": 
						{ 
							hexVal = ""; 
							MyMetadata.Brightness.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							hexVal = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0,2); 
							hexVal = Convert.ToInt32(hexVal,16).ToString(); 
							hexVal = hexVal + "00"; 
							MyMetadata.Brightness.DisplayValue = hexVal.Substring(0,1)+"."+hexVal.Substring(1,2); 
							break; 
						} 
 
						case "9204": 
						{ 
							MyMetadata.ExposureBias.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.ExposureBias.DisplayValue = BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString(); 
							break; 
						} 
 
						case "9205": 
						{ 
							hexVal = ""; 
							MyMetadata.MaxAperture.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							hexVal = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0,2); 
							hexVal = Convert.ToInt32(hexVal,16).ToString(); 
							hexVal = hexVal + "00"; 
							MyMetadata.MaxAperture.DisplayValue = hexVal.Substring(0,1)+"."+hexVal.Substring(1,2); 
							break; 
						} 
 
						case "9206": 
						{ 
							MyMetadata.SubjectDist.RawValueAsString =BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.SubjectDist.DisplayValue =Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
 
						case "9208": 
						{ 
							MyMetadata.LightSource.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.LightSource.DisplayValue = LookupEXIFValue("LightSource",BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString()); 
							break; 
						} 
 
						case "920a": 
						{ 
							hexVal = ""; 
							MyMetadata.FocalLength.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							hexVal = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0,2); 
							hexVal = Convert.ToInt32(hexVal,16).ToString(); 
							hexVal = hexVal + "00"; 
							MyMetadata.FocalLength.DisplayValue = hexVal.Substring(0,1)+"."+hexVal.Substring(1,2); 
							break; 
						} 
 
						case "a000": 
						{ 
							MyMetadata.FPXVer.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.FPXVer.DisplayValue = Value.GetString(MyPropertyItemList[index].Value).Substring(1,1)+"."+Value.GetString(MyPropertyItemList[index].Value).Substring(2,2); 
							break; 
						} 
 
						case "a001": 
						{ 
							MyMetadata.ColorSpace.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							if (BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString() == "1") 
								MyMetadata.ColorSpace.DisplayValue = "RGB"; 
							if (BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString() == "65535") 
								MyMetadata.ColorSpace.DisplayValue = "Uncalibrated"; 
							break; 
						} 
 
						case "a20e": 
						{ 
							MyMetadata.FocalXRes.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.FocalXRes.DisplayValue =BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString(); 
							break; 
						} 
 
						case "a20f": 
						{ 
							MyMetadata.FocalYRes.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.FocalYRes.DisplayValue = BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString(); 
							break; 
						} 
 
						case "a210": 
						{ 
							string aa; 
							MyMetadata.FocalResUnit.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							aa = BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString();; 
							if (aa == "1") MyMetadata.FocalResUnit.DisplayValue = "û�е�λ"; 
							if (aa == "2") MyMetadata.FocalResUnit.DisplayValue = "Ӣ��"; 
							if (aa == "3") MyMetadata.FocalResUnit.DisplayValue = "����"; 
							break; 
						} 
 
						case "a215": 
						{ 
							MyMetadata.ExposureIndex.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.ExposureIndex.DisplayValue = Value.GetString(MyPropertyItemList[index].Value); 
							break; 
						} 
 
						case "a217": 
						{ 
							string aa; 
							aa = BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value,0).ToString(); 
							MyMetadata.SensingMethod.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							if (aa == "2") MyMetadata.SensingMethod.DisplayValue = "1 chip color area sensor"; 
							break; 
						} 
 
						case "a301": 
						{ 
							MyMetadata.SceneType.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.SceneType.DisplayValue = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							break; 
						} 
 
						case "a302": 
						{ 
							MyMetadata.CfaPattern.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							MyMetadata.CfaPattern.DisplayValue = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value); 
							break; 
						} 
 
 
 
					} 
					#endregion 
                     
					index++; 
				} 
			} 
			#endregion

			MyMetadata.XResolution.DisplayValue = MyImage.HorizontalResolution.ToString();
			MyMetadata.YResolution.DisplayValue = MyImage.VerticalResolution.ToString();
			MyMetadata.ImageHeight.DisplayValue = MyImage.Height.ToString();
			MyMetadata.ImageWidth.DisplayValue = MyImage.Width.ToString();
			MyImage.Dispose();
			return MyMetadata;
		}
		#endregion

		#region Exif���ݴ���
		/// <summary>
		/// ��ȡLong�͵�10��������
		/// </summary>
		/// <param name="longValue">16�������ݵ��ַ�����ʾ</param>
		/// <returns>Double��10��������</returns>
		private static double GetLongValue(string[] longValueArr)
		{
			int length = longValueArr.Length;
			//string strValue = ""; 
			double longValue = 0.0;
			for(int i = 0 ; i < length ; ++i)
			{
				longValue += Double.Parse(Convert.ToInt64(longValueArr[i],16).ToString("d")) * Math.Pow(16,i*2);
			}

			return longValue;
		}

		/// <summary>
		/// ��ȡLong�͵�10��������
		/// </summary>
		/// <param name="longValueHigh">Long�����ݸ�λ</param>	
		/// <param name="longValueLow">Long�����ݵ�λ</param>	
		/// <returns>Double��10��������</returns>
		private static double GetLongValue(string longValueLow , string longValueHigh)
		{
			//int length = longValueArr.Length;
			double longValue = 0.0;
			longValue =Double.Parse(Convert.ToInt64(longValueLow,16).ToString("d")) + Double.Parse(Convert.ToInt64(longValueHigh,16).ToString("d")) * Math.Pow(16,2);			
			
			return longValue;
		}

		/// <summary>
		/// ����Rational����
		/// </summary>
		/// <param name="numerator">����</param>
		/// <param name="denominator">��ĸ</param>
		/// <returns></returns>
		private static double GetRationalValue(double numerator , double denominator)
		{
			return (numerator / denominator);
		}
		#endregion
	}
}
