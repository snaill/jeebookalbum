/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.MainPanel = function() {
	  
	// create the Grid
	this.grid = new Ext.app.StoreGrid();

	Ext.app.MainPanel.superclass.constructor.call(this, {
		id 		  : 'MainPanel_Id',
		region    : 'center',
		margins   : '3 3 3 0', 
		layout    : 'border',
		defaults  : {
			autoScroll : true
		},
		tbar:new Ext.Toolbar([new Ext.Action({
			id:'btn_addFolder',
			text: Ext.app.Resource.Toolbar.AddFolder,
			disabled : false,
			tooltip: {title:'Add folder',text:'Add sub folder to current path.'},
			enableToggle : true,
			handler: this.onAddFolder,
			scope:this
		}), '-', 
		new Ext.Action({
			id:'btn_addFile',
			text:Ext.app.Resource.Toolbar.AddFile,
			disabled : false,			
			tooltip: {title:'Add document',text:'Upload document to current folder.'},
			enableToggle : true,
			handler: this.onAddFile,
			scope:this
		}), '-',
		new Ext.Action({
			id:'btn_rename',
			text:Ext.app.Resource.Toolbar.Rename,
			disabled : true,			
			tooltip: {title:'Rname',text:'Rname current folder or document.'},
			enableToggle : true,
			handler: this.onRename,
			scope:this
		}), '-',
		new Ext.Action({
			id:'btn_delete',
			text:Ext.app.Resource.Toolbar.Delete,
			disabled : true,			
			tooltip: {title:'Delete',text:'Delete current folder or document.'},
			enableToggle : true,
			handler: this.onDelete,
			scope:this
		})]),
		items   : [this.grid, 
				  new Ext.app.ContentPanel()
		]
	});
};

Ext.extend(Ext.app.MainPanel, Ext.Panel, {
	onNotify : function( event ){
		Ext.app.ContentPanel.getObj().onNotify( event );
	},
	updateToolButton : function( o, action ){
		if ( o.disabled != null )	{
			action.setDisabled(o.disabled);
		}
	},
	updateToolbar : function( o ){
		if ( o.addFolder != null )	{
			this.updateToolButton( o.addFolder, Ext.getCmp('btn_addFolder') ); 
		}
		
		if ( o.addFile != null )	{
			this.updateToolButton( o.addFile, Ext.getCmp('btn_addFile') ); 
		}
				
		if ( o.rename != null )	{
			this.updateToolButton( o.rename, Ext.getCmp('btn_rename') ); 
		}
		
/* 		if ( o.delete != null )	{
			this.updateToolButton( o.delete, Ext.getCmp('btn_delete') ); 
		} */
	},
	onAddFolder : function(item){
		Ext.app.ContentPanel.getObj().showPanel( new Ext.app.AddFolderPanel(), Ext.getCmp('btn_addFolder') );
	},
	onAddFile : function(item){
		Ext.app.ContentPanel.getObj().showPanel( new Ext.app.AddFilePanel(), Ext.getCmp('btn_addFile') );
	},
	onRename : function(){
		Ext.app.ContentPanel.getObj().showPanel( new Ext.app.AddFilePanel(), Ext.getCmp('btn_rename') );	
	},
	onDelete : function(){
		Ext.app.ContentPanel.getObj().showPanel( new Ext.app.AddFilePanel(), Ext.getCmp('btn_delete') );	
	}	
});

Ext.app.MainPanel.getObj = function(){
	return Ext.getCmp('MainPanel_Id');
};
