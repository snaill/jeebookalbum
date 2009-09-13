/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.AddFilePanel = function() {

	Ext.app.AddFilePanel.superclass.constructor.call(this, {
		id 			: 'AddFilePanel_Id',
        title		: 'AddFilePanel',
        fileUpload	: true,
        frame		: true,
        bodyStyle	: 'padding: 10px 10px 0 10px;',
        labelWidth	: 60,
        defaults	: {
            anchor: '95%',
            allowBlank: false,
            msgTarget: 'side'
        },
        items		: [{
			id 	: 'AddFilePanel_Path_Id',
            xtype: 'hidden',
			hidden  : true
        },{
			id 	: 'AddFilePanel_Name_Id',
            xtype: 'textfield',
            fieldLabel: 'Name'
        },{
            xtype: 'fileuploadfield',
            emptyText: 'Select a document',
            fieldLabel: 'Document',
            buttonCfg: {
                text: '',
                iconCls: 'upload-icon'
            }
        },{
            xtype: 'textarea',
            hideLabel: true,
            name: 'remark',
            anchor: '100% -53'  // anchor width by percentage and height by raw adjustment
        }],
        buttons		: [{
            text: 'Upload',
            handler: function(){
				var name = Ext.getCmp('AddFilePanel_Name_Id').getValue();
				var path = Ext.getCmp('AddFilePanel_Path_Id').getValue();
				var url = './ashx/addFile.ashx';
				var form = Ext.getCmp('AddFilePanel_Id').getForm();
                if(form.isValid()){
	                form.submit({
	                    url: url,
						params : { name : name, path : path },
						method : 'GET',
	                    waitMsg: 'Uploading your document...',
						success: function(sender, o){
							Ext.app.StoreGrid.getObj().refresh();
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
				var form = Ext.getCmp('AddFilePanel_Id').getForm();
                form.reset();
            }
        }]
    });
	
	this.updatePanel();
};

Ext.extend(Ext.app.AddFilePanel, Ext.FormPanel, {
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
		
		this.setTitle( 'Upload document to '+ path );
		Ext.getCmp('AddFilePanel_Path_Id').setValue( path );
	}
} );