/***************************************************************
 * ���V�X�e������ JobzRacoo�V�X�e��
 * ���v���O������ ��{���
 * ���t�@�C������ BaseInfo.cs
 * ���쐬�ҁ�     MDCR
 * ���쐬����     2015/05/27
***************************************************************/

namespace Common.Info
{
    /// <summary>
    /// ��{���
    /// </summary>
    public class BaseInfo
    {

        /// <summary>
        /// ���ʃV�X�e�����
        /// </summary>
        private CommonSystemInfo _CommonSystemInfo;

        /// <summary>
        /// ���ʃV�X�e�����
        /// </summary>
        public CommonSystemInfo CommonSystemInfo
        {
            get
            {
                return _CommonSystemInfo;
            }
            set
            {
                _CommonSystemInfo = value;
            }
        }

        /// <summary>
        /// ���ID
        /// </summary>
        private string _strGamenID;

        /// <summary>
        /// ���ID
        /// </summary>
        public string GamenID
        {
            get
            {
                return _strGamenID;
            }
            set
            {
                _strGamenID = value;
            }
        }

        /// <summary>
        /// ��ʖ�
        /// </summary>
        private string _strGamenName;

        /// <summary>
        /// ��ʖ�
        /// </summary>
        public string GamenName
        {
            get
            {
                return _strGamenName;
            }
            set
            {
                _strGamenName = value;
            }
        }

        /// <summary>
        /// IP�A�h���X
        /// </summary>
        private string _strIPAddress;

        /// <summary>
        /// IP�A�h���X
        /// </summary>
        public string session_IP
        {
            get
            {
                return _strIPAddress;
            }
            set
            {
                _strIPAddress = value;
            }
        }

        /// <summary>
        /// PC��
        /// </summary>
        private string _strPCName;

        /// <summary>
        /// PC��
        /// </summary>
        public string session_PCName
        {
            get
            {
                return _strPCName;
            }
            set
            {
                _strPCName = value;
            }
        }
    }
}
