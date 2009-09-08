/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.SearchPanel = function() {
	Ext.app.SearchPanel.superclass.constructor.call(this, {
		id:'SearchPanel_Id',
		layout:'table',
		border:false,
		autoheight:true,
		layoutConfig: {
			columns: 2
		},
		items:[{
			id : 'SearchKey_Id',
			xtype: 'textfield',
            hideLabel: true,
            emptyText: Ext.app.Resource.EmptyText.SearchKey,
			name:'email',
			width:200
		}, new Ext.Button({
			text : Ext.app.Resource.Button.Search,
			handler : this.onSearch,
			scope :	this
		})],
		keys: [{    
			key:Ext.EventObject.ENTER,   
			fn: this.onSearch,   
			scope:this
		}] 
	});
};

Ext.extend(Ext.app.SearchPanel, Ext.FormPanel, {
	onSearch : function() {
		Ext.app.StoreGrid.getObj().search( Ext.app.StoreTree.getObj().getCurrentPath(), 
			Ext.getCmp('SearchKey_Id').getValue() );
	}
});