/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */

Ext.app.StoreGridReader = function() {
	  
    var meta = {};
    var recordType = [
		{name: 'name'},
		{name: 'size'},
		{name: 'time'},
		{name: 'id'},
		{name: 'remark'}
	];

    Ext.app.StoreGridReader.superclass.constructor.call(this, meta, recordType);
};

Ext.extend(Ext.app.StoreGridReader, Ext.data.JsonReader, {
    readRecords : function(o){
    	var recordType = this.recordType, fields = recordType.prototype.fields;
    	var totalRecords = o.total, success = o.success;
    	var records = [];
        for(var i = 0, len = o.data.length; i < len; i++) {
	        var n = o.data[i];
	        var values = {};
			
	        for(var j = 0, jlen = fields.length; j < jlen; j++){
	            var f = fields.items[j];
	            values[f.name] = n[f.name];
	        }
			
	        var record = new recordType(values, null);
	        record.node = n;
	        records[records.length] = record;
	    }

	    return {
	        success : success,
	        records : records,
	        totalRecords : totalRecords || records.length
	    };
    }
} );