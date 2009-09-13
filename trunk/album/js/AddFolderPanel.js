/*
 * Jeebook Store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.AddFolderPanel = function() {

	Ext.app.AddFolderPanel.superclass.constructor.call(this, {
		id : 'AddFolderPanel_Id',
		title: 'AddFolderPanel',
        frame: true,
        bodyStyle: 'padding: 10px 10px 0 10px;',
        labelWidth: 50,
        defaults: {
            anchor: '95%',
            allowBlank: false,
            msgTarget: 'side'
        },
        items: [{
			id : 'AddFolderPanel_Path_Id',
            xtype: 'hidden',
			hidden  : true
        },{
			id : 'AddFolderPanel_Name_Id',
            xtype: 'textfield',
            fieldLabel: 'Name'
        }],
        buttons: [{
            text: 'Create',
            handler: function(){
				var name = Ext.getCmp('AddFolderPanel_Name_Id').getValue();
				var path = Ext.getCmp('AddFolderPanel_Path_Id').getValue();
				var url = './ashx/addfolder.ashx';
				var form = Ext.getCmp('AddFolderPanel_Id').getForm();
                if(form.isValid()){
	                form.submit({
	                    url: url,
						params : { name : name, path : path },
						method : 'GET',
	                    waitMsg: 'Creating new folder...',
	                    success: function(sender, o){
							Ext.app.StoreTree.getObj().refresh();
							Ext.app.ContentPanel.getObj().clear();
							Ext.app.Navigatebar.getObj().setStatus(true, 'Create folder success.');
	                    },
						failure:function(sender, o ) {
							Ext.app.Navigatebar.getObj().setStatus(false, Ext.app.Error.getMessage( o.result.fault ) );
						}
	                });
                }
            }
        },{
            text: 'Reset',
            handler: function(){
				var form = Ext.getCmp('AddFolderPanel_Id').getForm();
                form.reset();
            }
        }]
    });

	this.updatePanel();
};

Ext.extend(Ext.app.AddFolderPanel, Ext.FormPanel, {
	onNotify : function( event ) {
		if ( event.id != Ext.app.Event.FolderChanged )
			return;
		
		this.updatePanel( event );
	},
	updatePanel : function( event ) {
		var path = '';
		if ( event != null )
			path = Ext.app.StoreTree.getObj().getPath( event.node );
		else
			path = Ext.app.StoreTree.getObj().getCurrentPath();
		
		this.setTitle( 'Add folder to '+ path );
		Ext.getCmp('AddFolderPanel_Path_Id').setValue( path );
	}
});