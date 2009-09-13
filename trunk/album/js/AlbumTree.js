/*
 * Jeebook Album 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.AlbumTree = function() {
	
	Ext.app.AlbumTree.superclass.constructor.call(this, {
		id			: 'AlbumTree_Id',
	//	title       : 'Folders',
		region      : 'west',
		split       : true,
		useArrows	: true,
        animate		: true,
		width       : 200,
		collapsible : true,
		collapseMode:'mini',
		margins     : '3 0 3 3',
		cmargins    : '3 3 3 3',
		autoScroll 	: true,
		loader		: new Ext.app.AlbumTreeLoader({
			dataUrl : 'ashx/albums.ashx',
       		clearOnLoad:false
       	}),
		root 		: new Ext.tree.AsyncTreeNode({
			id:'root',
			text:'Album'
		})
	});

	this.on('click', function( node ) {
		Ext.app.StoreGrid.getObj().load( this.getPath(node) );
		
		var event = {};
		event.id = Ext.app.Event.FolderChanged;
		event.node = node;
		Ext.app.MainPanel.getObj().onNotify( event );
	}, this );
	
	this.root.expand();
};

Ext.extend(Ext.app.AlbumTree, Ext.tree.TreePanel, {
	getPath : function( node ) {
		var p = node.getPath() + '/';
		// delete /root
		return p.substring( 5 );
	},
	getCurrentPath : function(){
		var node = this.getSelectionModel().getSelectedNode();
		if ( !node )
			node = this.root;
		return this.getPath( node );
	},
	refresh : function(){
		var node = this.getSelectionModel().getSelectedNode();
		if ( !node )
			node = this.root;
		node.reload();
	}
} );

Ext.app.AlbumTree.getObj = function(){
	return Ext.getCmp('AlbumTree_Id');
};