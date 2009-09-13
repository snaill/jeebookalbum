/*
 * Jeebook Album 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.AlbumTreeLoader = Ext.extend( Ext.tree.TreeLoader, {
    requestData : function(node, callback){
        if(this.fireEvent("beforeload", this, node, callback) !== false){
			var nodePath = Ext.app.AlbumTree.getObj().getPath( node );
            this.transId = Ext.Ajax.request({
                method:'GET',
                url: this.dataUrl||this.url,
                success: this.handleResponse,
                failure: this.handleFailure,
                scope: this,
                argument: {callback: callback, node: node},
                params: { path : nodePath }
            });
        }else{
            // if the load is cancelled, make sure we notify
            // the node that we are done
            if(typeof callback == "function"){
                callback();
            }
        }
    }
});