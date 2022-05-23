/***************************************************************
 * ���V�X�e������ JobzRacoo�V�X�e��
 * ���v���O������ ���ʃV�X�e�����
 * ���t�@�C������ CommonSystemInfo.cs
 * ���쐬�ҁ�     MDCR
 * ���쐬����     2015/05/27
 ***************************************************************/
using System;
using System.Data;
using System.Collections;

namespace Common.Info
{
    /// <summary>
    /// ���ʃV�X�e�����
    /// </summary>
    [Serializable]
    public class CommonSystemInfo
    {

        #region "�v���p�e�B�ϐ�"

        /// <summary>
        /// ���[�U�[�R�[�h
        /// </summary>
        private string _strcUserID;

        /// <summary>
        /// ���[�U�[����
        /// </summary>
        private string _strsUserName;

        #endregion

        #region "�v���p�e�B"

        /// <summary>
        /// ���[�U�[�R�[�h
        /// </summary>
        public string session_UserID
        {
            set
            {
                _strcUserID = value;
            }
            get
            {
                return _strcUserID;
            }
        }

        /// <summary>
        /// ���[�U�[����
        /// </summary>
        public string session_UserName
        {
            set
            {
                _strsUserName = value;
            }
            get
            {
                return _strsUserName;
            }
        }

        #endregion
    }
}
