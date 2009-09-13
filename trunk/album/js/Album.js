/*
 * Jeebook store 1.0
 * Copyright(c) 2008, Jeebook.com
 * snaill@jeebook.com
 * 
 * http://www.jeebook.com
 */
Ext.BLANK_IMAGE_URL = '../ext-3.0.0/resources/images/default/s.gif';
Ext.app.Resource = new Object();
Ext.app.UserState = null;

Ext.onReady(function(){

	Ext.QuickTips.init();
	Ext.state.Manager.setProvider( new Ext.state.SessionProvider( { state: Ext.appState } ) );
	
	var language;
	if (navigator.appName == 'Netscape')
		language = navigator.language;
	else
		language = navigator.browserLanguage;

	if (language.indexOf('zh') > -1) 
		Ext.app.Resource = new Ext.app.zh_CN();
	else 
		Ext.app.Resource = new Ext.app.en_US(); 

	var header = new Ext.Panel({
		layout 	: 'border',
		region : 'north',
		height : 115,
		items 	: [
			new Ext.app.Navigatebar(),
			{
				region : 'west',
				border : false,
				width	: 180,
				html : '<img src=\"images/logo.png\" />'
			}, 
			new Ext.Panel({
				region	: 'east',
				layout	: 'table',
				width 	: 468,
				border	: false,
				layoutConfig: { 
					columns: 1
				},
				items : [
					{ 
						border : false, 
						height : 10 
					}, {
						border	: false,
						width : 468,
						height : 60, 
						el : 'adbox'
					}
				]
			}),
			new Ext.Panel({
				region	: 'center',
				layout	: 'table',
				border	: false,
				layoutConfig: { 
					columns: 1
				},
				items : [
					{ 
						border : false, 
						height : 40 
					}, 
					new Ext.app.SearchPanel()
				]
			})
		]
	});

	var tree = new Ext.app.AlbumTree();
	var main = new Ext.app.MainPanel();

	var viewport = new Ext.Viewport({
		layout : 'border',
		items : [ header, tree, main ]	
	});
	
	viewport.doLayout();

	setTimeout(function(){
        Ext.get('loading').remove();
        Ext.get('loading-mask').fadeOut({remove:true});
    }, 250);
});