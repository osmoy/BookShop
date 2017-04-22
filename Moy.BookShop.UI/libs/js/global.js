// JavaScript Document

var $=function(id){return document.getElementById(id);}


/*图片正比例缩放 onload="set_pic_size(this,200,200);" */
function set_pic_size(imgid,maxh,maxw){
	var img = (typeof(imgid)=="string")?document.getElementById(imgid):imgid;
	var load_img = new Image();
	var onloadFun = function(){
		public_img_load(load_img,maxh,maxw);
		img.setAttribute("height",load_img.height);
		img.setAttribute("width",load_img.width);
	};
	load_img.onload = onloadFun;
	load_img.src = img.src;
}

function public_img_load(imgid,height,width){
	var imgEle=(typeof(imgid)=="string")?document.getElementById(imgid):imgid;
	if(imgEle.height==0||imgEle.width==0){
		imgEle.setAttribute("height",height);
		imgEle.setAttribute("width",width);
		return true;
	}
	if(imgEle.height>height||imgEle.width>width){
		if((imgEle.height/imgEle.width)<(height/width)){
			imgEle.setAttribute("height",(imgEle.height/imgEle.width)*width);
			imgEle.setAttribute("width",width);
			return true;
		}
		else{
			imgEle.setAttribute("width",(imgEle.width/imgEle.height)*height);
			imgEle.setAttribute("height",height);
			return true;
		}
	}
}