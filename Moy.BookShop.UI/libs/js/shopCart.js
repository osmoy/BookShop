/*
 * Numbox 1.4, jQuery plugin
 *
 * Copyright(c) 2016, xusimin@itonghui.org
 *
 * 购物车
 */   
(function($){
    var click = "click",
	change = "change",
	keyup = "input",
	press = "keypress",
	parent = ".xsm-numbox",//父级Class
	plus = ".xsm-btn-numbox-plus,.xsm-numbox-btn-plus",//加
	minus = ".xsm-btn-numbox-minus,.xsm-numbox-btn-minus",//减
	input = ".xsm-input-numbox,.xsm-numbox-input",//输入框
	intRegex = /^[\-]?\d+$/,
	douRegex = /^[\-]?([0-9]*)?([\.]([0-9]*))?$/,
	pointRegex = /^[\-][0-9]+[\.]$|^([0-9]*)?[\.]$/,
	Numbox = function(element, options){
	    this.input = element.find(input).eq(0);
	    this.plus = element.find(plus).eq(0);
	    this.minus = element.find(minus).eq(0);
	    this.money = this.input.hasClass('money');
	    this.regex = this.money ? douRegex : intRegex;
	    this.options = options;
	    this.initEvent();
	};
    Numbox.prototype.initEvent = function () {
        var thiS = this;
        thiS.checkValue();
        thiS.plus.bind(click, function(){
            var e = parseInt(thiS.input.val()) + thiS.options.step;
            thiS.input.val(e), thiS.input.trigger(change)
        }), thiS.minus.bind(click, function(){
            var e = parseInt(thiS.input.val()) - thiS.options.step;
            thiS.input.val(e), thiS.input.trigger(change)
        }), thiS.input.bind(change, function(){
            thiS.value2 = thiS.input.val();
            thiS.checkValue();
            if (typeof thiS.options.onChange === 'function') {
                try {
                    thiS.options.onChange(thiS.input);
                } catch (err) {
                    console.log('There is an error in onChange callback');
                    console.error(err);
                }
            }
        }), thiS.input.bind(keyup, function(){
            if(thiS.input.val()!='') {
                if(thiS.checkValue()) {
                    thiS.input.trigger(change)
                }
            }
        }), thiS.input.bind(press, function(event){
            var keyCode = event.keyCode||event.charCode;
            if(keyCode==46 && !thiS.money){
                return false;
            }
            if(keyCode&&(keyCode<48||keyCode>57)&&keyCode!=8&&keyCode!=45&&keyCode!=46&&keyCode!=37&&keyCode!=39){
                return false;
            }
        })
    };
    Numbox.prototype.checkValue = function () {
        var thiS = this,
		val = thiS.input.val(),
		end = false;
        if (!thiS.regex.test(val)) {
            thiS.input.val(thiS.options.min || 0),
			thiS.minus.attr('disabled', null != thiS.options.min);
            end = true;
        } else {
            if(!pointRegex.test(val)) {
                var _val = parseFloat(val);
                null != thiS.options.max && !isNaN(thiS.options.max) && _val >= thiS.options.max ? (val = thiS.options.max, thiS.plus.attr('disabled', true), end = true) : thiS.plus.attr('disabled', false),
				null != thiS.options.min && !isNaN(thiS.options.min) && _val <= thiS.options.min ? (val = thiS.options.min, thiS.minus.attr('disabled', true), end = true) : thiS.minus.attr('disabled', false);
            }
            if(thiS.money && thiS.options.precision!=undefined) {
                var i = -1;
                try {i = val.indexOf('.');} catch (e) {}
                if(i >= 0 && val.substring(i+1).length>thiS.options.precision) {
                    val = val.substring(0,i+thiS.options.precision+1);
                    end = true;
                }
            }
            if(/^[\.]([0-9]*)?$/.test(val)) {
                val = 0+val;
            } else if (/^[\-]?[0][0-9]/.test(val)) {
                val = parseFloat(val);
            }
            thiS.input.val(val);
        }
        if(end) {
            return val != thiS.value2;
        }
    };
    $.fn.numbox = function(opts) {
        return this.each(function(i,obj) {
            var defaults = {step:1};
            var options = {};
            options.step = obj.getAttribute("data-numbox-step"),
			options.min = obj.getAttribute("data-numbox-min"),
			options.max = obj.getAttribute("data-numbox-max"),
			options.precision = obj.getAttribute("data-numbox-precision");
            options.step = options.step ? parseInt(options.step) : undefined,
			options.min = options.min ? parseFloat(options.min) : undefined,
			options.max = options.max ? parseFloat(options.max) : undefined,
			options.precision = options.precision ? parseInt(options.precision) : undefined;
            if (!obj.numbox) {
                obj.numbox = new Numbox($(obj), $.extend(true, defaults, opts, options));
            } else {
                obj.numbox.options=$.extend(true, defaults, opts, options);
            }
        });
    }, $().ready(function() {
        $(parent).numbox()
    })
})(jQuery);

$(function () {
    checkboxED('.checkAll','.checkEX')
    $('.xsm-numbox').numbox({
        onChange: function(input){
            var box = input.parents('tr').find('input:checkbox')
            if(!box.prop('checked')){
                box.trigger('click')
            }
            sum()
        }
    })
    $('.deleteCart').click(function(){
        $(this).parents('tr').remove()
        sum()
    })
    //批量删除
    $('.dlt_all').bind('click', function () {
        var box = $(this).parents('.collect-pro-list').find('.checkEX:checked');
        if(box.length==0){
            alert('请先选择商品');
            return false;
        }
        box.each(function(){
            $(this).parents('tr').remove()
        })
        sum()
    });
});
var checkboxED = function(allCKD,repeatCKD){
    //全选
    $(allCKD).click(function(){
        $(repeatCKD).prop("checked",$(this).prop("checked"));
        sum()
    });
    //单选
    $(repeatCKD).click(function(){
        $(allCKD).prop("checked",$(repeatCKD+":checked").length==$(repeatCKD).length);
        sum()
    });
};
//乘法
function accMul(arg1,arg2){
    var m=0,s1=arg1.toString(),s2=arg2.toString();
    try{m+=s1.split(".")[1].length}catch(e){}
    try{m+=s2.split(".")[1].length}catch(e){}
    return Number(s1.replace(".",""))*Number(s2.replace(".",""))/Math.pow(10,m)
}

function sum() {
    $('.collect-pro-list').each(function(){
        var checkBox = $(this).find('.checkEX:checked');
        var total = 0;
        var money =0;
        $.each(checkBox, function () {
            var tr=$(this).parents('tr');
            var price = parseFloat(tr.find(".unitPrice").text());
            var number = parseInt(tr.find(".xsm-input-numbox").val());
            var pn = accMul(price,number);
            tr.find(".subTotal").text(pn.toFixed(2));
            money += pn;
            total++;
        });
        $(this).find('.totalNum').text(total);
        $(this).find('.shopping-allmoney').text("￥" + money.toFixed(2));
    });
    $('.collect-pro-list').each(function(){
        if($(this).find('.checkEX')==0){
            $(this).find('.checkAll').prop('checked',false)
        }else{
            $(this).find('.checkAll').prop('checked',$(this).find('.checkEX').length==$(this).find('.checkEX:checked').length)
        }
    });	
}