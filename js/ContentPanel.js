/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.ContentPanel = function() {
  
	this.compId = "";
	this.panel = null;
	this.actionButton = null;

	Ext.app.ContentPanel.superclass.constructor.call(this, {
		id:'ContentPanel_Id',
		layout:'fit',
		height: 250,
		split: true,
		border:false,
		hidden:true,
		region:'south'
	});
};

Ext.extend(Ext.app.ContentPanel, Ext.Panel, {
	onNotify : function( event )	{
		if ( this.panel != null )
			this.panel.onNotify( event );
	},
	clear : function()	{
		// clear old state
		this.remove(this.compId);

		this.compId = null;
		this.panel = null;

		if ( this.actionButton != null )
		{
			this.actionButton.toggle( false );
			this.actionButton = null;
		}
		
		this.hide();
		this.ownerCt.doLayout();
	},
	showPanel : function( panel, actionButton ) {
		// clear old state
		this.remove(this.compId);
		if ( this.panel != null && this.panel.id == panel.id )
		{
			this.compId = null;
			this.panel = null;
			this.actionButton = null;

			this.hide();
			this.ownerCt.doLayout();
			return;
		}
		else if ( this.actionButton != null )
		{
			this.actionButton.toggle( false );
		}

		// create new panel
		var comp = null;
		if ( panel == null )
		{
			this.panel = null;
			return;
		}
			
		comp = this.add(panel);
		
		//
		this.compId = comp.id;
		this.panel = panel;
		this.actionButton = actionButton;

		//
		this.show();
		this.ownerCt.doLayout();
	}
});

Ext.app.ContentPanel.getObj = function(){
	return Ext.getCmp('ContentPanel_Id');
};
