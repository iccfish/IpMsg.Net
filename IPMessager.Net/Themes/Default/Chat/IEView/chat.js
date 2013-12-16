/**
 * 初始化显示信息
 * 在这个函数中将会与浏览器环境进行交互取得用户资料
 */
function initInfo() {
	var info = eval("(" + window.external.GetHostInfo() + ")");
	$("#head h5").html("与 " + info.username + " 对话中 (" + info.ip + "/" + info.group + ")");
	$("#head p").html("对方 <u>" + (info.isabsence ? "离开" : "在线") + "</u>"
		+ (info.isabsence ? " (" + info.statemsg + ")" : "") + "，" + (info.enc ? "对方支持加密通讯" : "对方不支持加密通讯，消息可能会被嗅探")
		+ "，双击标签页关闭此聊天窗口。<br />欢迎使用飞鸽传书.Net，如果您有任何建议或问题，请邮件告知作者：<a href=\"#\" onclick=\"window.external.GoUrl(\'mailto:fishcn@foxmail.com\');\">fishcn@foxmail.com</a>。");
	host = info;
	//初始化事件
	$(".umsg input.openPackage").live("click", openPackage);
}
$(initInfo);

/**
 * 添加提示信息
 * @param {Object} 要显示信息内容
 */
function applyTipInfo(msg) {
	var q = $("<div class='tip'>" + msg + "</div>");
	$("#chat").append(q);
	autoScroll();
}

/**
 * 自动将当前浏览器窗口滚动到结尾
 */
function autoScroll() {
	window.scroll(0, document.documentElement.scrollHeight);
}

/**
 * 根据指定ID的元素进行定位
 * @param {Object} 要定位的元素ID
 */
function hashLocate(id) {
	location.hash = "#" + id;
}

/**
 * 清空整个信息区域
 */
function clearChatHistory() {
	$("#chat").empty();
}

//--------------------------------------------------------------------------------------------------------------------------

var host;
var myIndex = 0;

//--------------------------------------------------------------------------------------------------------------------------
//捕捉状态变化事件
function absenceModeChange(s, i) {
	applyTipInfo("对方状态变化为 <strong>" + (s ? "离开" : "在线") + "</strong> " + (s ? "(" + i + ")" : "") + "");
}
//昵称变化
function nickNameChange(a, b) {
	applyTipInfo("对方个人信息变化为 <strong>" + a + "</strong> (" + b + ")");
}
//打开封包
function openPackage() {
	var inputObj = $(this);
	var pid = inputObj.attr("pid");

	var msgDiv = $("#umsg_" + pid);
	$(".packed").slideUp();
	$(".content").slideDown();

	//发送阅读信息
	window.external.SendOpenSignal(pid);
}
//收到对方消息
function messageReceied(packageNo, isEnc, recvTime, content, isPacked, isPassword, autoOpenPackage, isAutoSend, ap) {
	var html = "<div class='umsg' id='umsg_" + packageNo + "'>";
	html += "<h5>" + host.username + " " + recvTime + "  [编号 " + packageNo + "," + (isEnc ? "RSA加密" : "未加密") + (isPacked ? ",已封包" : "") + (isPassword ? ",密码阅读" : "") + "]</h5>";
	if (isPacked) {
		html += "<p class='packed'>消息已经封包，需要确认您阅读 <input pid='" + packageNo + "' type='button' class='openPackage' value='打开消息' /></p>";
	}
	html += "<p class='content " + (isPacked ? "hidden" : "") + "'>" + content + "</p>";
	var tip = "";
	if (isAutoSend) {
		tip += "<li>这是一条自动回复的信息</li>";
	}
	if (ap != "") {
		tip += "<li>本消息根据您的设置，已经于 <strong>" + ap + "</strong> 自动回复</li>";
	}

	if (tip != '') html += "<div class='tip'><ul>" + tip + "</ul></div>";
	html += "</div>";

	$("#chat").append($(html));
	hashLocate("umsg_" + packageNo);
}
//发送消息给对方
function messageSend(un, sendTime, content) {
	var html = "<div class='mmsg' id='mmsg_" + myIndex + "'>";
	html += "<h5>" + host.username + " " + sendTime + "</h5>";
	html += "<p class='content'>" + content + "</p>";
	html += "</div>";

	$("#chat").append($(html));
	hashLocate("mmsg_" + (myIndex++));
}





//=======================================================文件传输部分=======================================================

var fileState = ["等待中", "初始化中", "传输中", "传输失败", "已完成", "正在取消", "已取消"];
var errDesc = ["打开文件", "打开目录", "创建目录", "创建文件", "写入数据"];

function fileOpError(type, path) {
	applyTipInfo("文件读取或创建失败，操作：<strong>" + errDesc[type] + "</strong>，文件(夹)：<strong>" + path + "</strong>");
}

//=======================================================文件发送部分=======================================================

var filesendIndex = 0;

//拖动文件准备发送
function fileSendingPre(isDirectory, path, size) {
	var obj = $("#filesend_pre");
	if (obj.length == 0) {
		var html = new Array();
		html.push('<div class="filesend" id="filesend_pre">');
		html.push('<h5>准备发送文件</h5><div class="list"></div><p>您可以继续拖动文件到这个窗口来增加到列表 <input type="button" class="send" value="确定发送" />&nbsp;<input type="button" class="cancel" value="取消发送" /></p></div>');

		html = $(html.join(""));
		$("input.send", html).click(submitFileSendDiv);
		$("input.cancel", html).click(clearFileSendPreDiv);
		$("#chat").append(html);
		autoScroll();

		obj = html;
	}
	var newEntry = "[" + (++filesendIndex) + "] <strong>" + path + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;" + (isDirectory ? "[文件夹]" : size) + "<br />";
	$(".list", obj).append(newEntry);
	hashLocate("filesend_pre");
}

/*确定发送文件*/
function submitFileSendDiv() {
	window.external.SubmitFileSend();
}

function clearFileSendPreDiv() {
	window.external.ClearFileSendQueue();
	$("#filesend_pre").remove();
	filesendIndex = 0;
}

//请求发送文件
function fileSendingRequest(v) {
	v = eval("(" + v + ")");
	var html = new Array();
	html.push('<div class="filesend" id="filesend_' + v.pkgid + '">');
	html.push('<h5>请求发送文件  [' + v.pkgid + ']</h5><div class="list"><table><tr><th>文件数</th><th>平均速度</th><th>总大小</th><th>已传输</th><th>进度</th><th>已用时间</th><th>剩余时间</th></tr>');
	for (var i in v.tasks) {
		var o = v.tasks[i];
		html.push('<tr idx="' + i + '"><td class="filename" colspan="6"><div style="width:0%;"></div><p>' + o.filename + '</p>&nbsp;</td><td class="state">等待ing</td></tr>');
		html.push('<tr idt="' + i + '"><td class="filenum">' + o.sended + '/' + o.filecount + '</td><td class="speed">----</td><td class="sizetotal">' + o.filesize + '</td><td class="sizesend">' + o.sizesended + '</td><td class="percentage">0%</td><td class="usedtime">' + o.timeused + '</td><td class="resttime">' + o.timerest + '</td></tr>');
	}
	html.push('</table></div><p>等待对方接收</p></div>');

	$("#chat").append($(html.join("")));
}

function fileSendingStateChange(pkgid, idx, filename, curName, state) {
	var obj = $("#filesend_" + pkgid);
	var td = $("tr[idx=" + (idx) + "]", obj);

	$(".state", td).html(fileState[state]);
	$(">p", obj).html(filename + " " + fileState[state]);

	if (state == 4) {
		td.remove();
		$("tr[idt=" + (idx) + "]", obj).remove();
	}

	if (state == 3) applyTipInfo("<strong>" + curName + "</strong> 发送失败，等待对方重新接收。");
	else if (state == 4) applyTipInfo("<strong>" + curName + "</strong> 发送成功。");
}

function fileSendingTaskFinished(pkgid) {
	setTimeout('$("#filesend_' + pkgid + '").remove();', 1000);
}

function fileSendingDiscard(pkgid, cnt) {
	applyTipInfo(host.username + " 已经忽略了您发送的文件 (编号 " + pkgid + ")，包含下列文件或文件夹：" + cnt);
	fileSendingTaskFinished(pkgid);
}

function fileSendingExpired(pkgid, dt) {
	applyTipInfo("您于 <strong>" + dt + "</strong> 发送给 " + host.username + " 的文件，对方超时未接收 (编号 " + pkgid + ")");
	fileSendingTaskFinished(pkgid);
}

function fileSendingProgressChange(v) {
	v = eval("(" + v + ")");
	var obj = $("#filesend_" + v.pkgid);
	var td = $("tr[idx=" + v.index + "]", obj);

	$(".filename div", td).css({ width: v.percentage + "%" });

	td = $("table tr[idt=" + v.index + "]", obj);
	$(".filenum", td).html(v.sended + "/" + v.filecount);
	$(".speed", td).html(v.speed);
	$(".sizetotal", td).html(v.filesize);
	$(".sizesend", td).html(v.sizesended);
	$(".percentage", td).html(v.percentage + " %");
	$(".usedtime", td).html(v.timeused);
	$(".resttime", td).html(v.timerest);

	if (v.state == 2) $(">p", obj).html("正在发送 " + v.filename);
}

/*收到对方文件发送请求*/
function receiveFileRequired(task, saveToSameLocation) {
	task = eval("(" + task + ")");
	if (task.isretry) {
		denyTask = task;
		setTimeout("_receiveFileRequired(null,"+saveToSameLocation+");", 1500);
	} else {
		_receiveFileRequired(task, saveToSameLocation);
	}
}

var denyTask = null;
/*收到对方文件发送请求*/
function _receiveFileRequired(task, saveToSameLocation) {
	if (task == null) {
		task = denyTask;
		denyTask = null;
	}
	var html = new Array();
	html.push('<div class="fileAttach" pkgid="' + task.pkgid + '"><h5>对方请求发送文件</h5><table>');
	html.push('<tr><th class="idx">序号</th><th class="filename">名称</th><th class="size">大小</th><th class="location">保存位置</th></tr>');

	var list = task.tasks;
	for (var i in list) {
		var t = list[i];

		html.push('<tr idx="' + i + '" pkgid="' + task.pkgid + '" tp="' + t.isfolder + '"><td>' + i + '</td><td>' + t.filename + '</td><td>' + t.filesize + (t.isfolder?"(文件夹)":"") + '</td><td><input type="text" readonly="readonly" class="pathtxt" value="' + t.path + '" /><input type="button" value="..." class="browser" /></td></tr>');
	}
	html.push('</table>');
	html.push('<p><input type="checkbox" />保存到同一个目录<input type="button" value="确定接收" class="ok" /><input type="button" value="忽略文件" class="cancel" /></p>');
	html.push('</div>');

	var cnt = $(html.join(""));
	$("#chat").append(cnt);

	//Init state
	$(":checkbox", cnt).click(function() { window.external.SelectSetSaveFolder(this.checked); })[0].checked = saveToSameLocation;

	//init call
	$(".browser", cnt).click(selectReceiveFilePath);
	$(".cancel", cnt).click(function() {
		window.external.CancelFileReceive(task.pkgid);
		cnt.remove();
	});
	$(".ok", cnt).click(function() {
		var ok = true;
		$(":text", cnt).each(function() { if (!ok) return; if ($(this).val() == "") { ok = false; alert("您尚未选择部分文件的路径!"); } });
		if (!ok) return;

		window.external.SubmitFileReceive(task.pkgid);
		cnt.remove();
	});
}

function selectReceiveFilePath() {
	var _btn = $(this);
	var _td = _btn.parent();
	var _tr = _td.parent();

	var idx = _tr.attr("idx");
	var pkgid = _tr.attr("pkgid");
	var _curpath = $(":text", _td).val();
	var _filename = $("td:eq(1)", _tr).html();
	var _filesize = $("td:eq(2)", _tr).html();
	var _tp = _tr.attr("tp");

	//	if (_tp == 0) {
	//		_curpath = window.external.SelectSavePath("请选择保存 " + _filename + "(" + _filesize + ") 的位置", _curpath);
	//	} else {
	_curpath = window.external.SelectSaveFolder("请选择保存 " + _filename + "(" + _filesize + ") 的位置", _curpath, pkgid + '', idx + '');
	//	}

	if (_curpath != null && _curpath != "") {
		$(":text", _td).val(_curpath);

		if ($(".fileAttach :checkbox")[0].checked) {
			$(".pathtxt", _tr.parent()).val(_curpath);
		}
	}
}


//接收文件
function fileRecvRequest(v) {
	v = eval("(" + v + ")");
	var html = new Array();
	html.push('<div class="filerecv" id="filerecv_' + v.pkgid + '">');
	html.push('<h5>接收文件  [' + v.pkgid + ']</h5><div class="list"><table><tr><th>文件数</th><th>平均速度</th><th>总大小</th><th>已传输</th><th>进度</th><th>已用时间</th><th>剩余时间</th></tr>');
	for (var i in v.tasks) {
		var o = v.tasks[i];
		html.push('<tr idx="' + i + '"><td class="filename" colspan="6"><div style="width:0%;"></div><p>' + o.filename + '</p>&nbsp;</td><td class="state">等待ing</td></tr>');
		html.push('<tr idt="' + i + '"><td class="filenum">' + o.sended + '/' + o.filecount + '</td><td class="speed">----</td><td class="sizetotal">' + o.filesize + '</td><td class="sizesend">' + o.sizesended + '</td><td class="percentage">0%</td><td class="usedtime">' + o.timeused + '</td><td class="resttime">' + o.timerest + '</td></tr>');
	}
	html.push('</table></div><p>正在连接</p></div>');

	$("#chat").append($(html.join("")));
}

function fileRecvStateChange(pkgid, idx, filename, curName, state) {
	var obj = $("#filerecv_" + pkgid);
	var td = $("tr[idx=" + (idx) + "]", obj);

	$(".state", td).html(fileState[state]);
	$(">p", obj).html(filename + " " + fileState[state]);

	if (state == 4) {
		td.remove();
		$("tr[idt=" + (idx) + "]", obj).remove();
	}

	if (state == 3) applyTipInfo("<strong>" + curName + "</strong> 接收失败，请稍后重试。");
	else if (state == 4) applyTipInfo("<strong>" + curName + "</strong> 接收成功。");
}

function fileRecvTaskFinished(pkgid) {
	setTimeout('$("#filerecv_'+pkgid+'").remove();', 1000);
}

function fileRecvProgressChange(v) {
	v = eval("(" + v + ")");
	var obj = $("#filerecv_" + v.pkgid);
	var td = $("tr[idx=" + v.index + "]", obj);

	$(".filename div", td).css({ width: v.percentage + "%" });

	td = $("table tr[idt=" + v.index + "]", obj);
	$(".filenum", td).html(v.sended + "/" + v.filecount);
	$(".speed", td).html(v.speed);
	$(".sizetotal", td).html(v.filesize);
	$(".sizesend", td).html(v.sizesended);
	$(".percentage", td).html(v.percentage + " %");
	$(".usedtime", td).html(v.timeused);
	$(".resttime", td).html(v.timerest);

	if (v.state == 2) $(">p", obj).html("正在接收 " + v.filename);
}

