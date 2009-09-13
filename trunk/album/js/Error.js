/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.Error = {};

Ext.app.Error.CODE_UNKNOWN = 0;
Ext.app.Error.CODE_PARAMETER = 1;
Ext.app.Error.CODE_PATHNOTEXIST = 2;
Ext.app.Error.CODE_LOGINFAILED = 3;
Ext.app.Error.CODE_FOLDERISEXISTED = 4;
Ext.app.Error.CODE_FILEISEXISTED = 5;

Ext.app.Error.getMessage = function( o ) {
	switch ( o.code )
	{
		case Ext.app.Error.CODE_PARAMETER:
			return Ext.app.Resource.Error.Parameter;
		case Ext.app.Error.CODE_PATHNOTEXIST:
			return Ext.app.Resource.Error.PathNotExist;
		case Ext.app.Error.CODE_LOGINFAILED:
			return Ext.app.Resource.Error.LoginFailed;
		case Ext.app.Error.CODE_FOLDERISEXISTED:
			return Ext.app.Resource.Error.FolderIsExisted;
		case Ext.app.Error.CODE_FILEISEXISTED:
			return Ext.app.Resource.Error.FileIsExisted;
		default:
			return o.message;
	}
};