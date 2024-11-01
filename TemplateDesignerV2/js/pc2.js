/*Modified locally by Saqib Ali*/
var ActualImHeight, ActualImWidth;
var croppedInstance = null;
(function () {
  // helper functions
  function is_touch_device() {
    return 'ontouchstart' in window || // works on most browsers
           'onmsgesturechange' in window; // works on ie10
  }

  function fill(value, target, container) {
    if (value + target < container)
      value = container - target;
    return value > 0 ? 0 : value;
  }

  function uri2blob(dataURI) {
      var uriComponents = dataURI.split(',');
      var byteString = atob(uriComponents[1]);
      var mimeString = uriComponents[0].split(':')[1].split(';')[0];
      var ab = new ArrayBuffer(byteString.length);
      var ia = new Uint8Array(ab);
      for (var i = 0; i < byteString.length; i++)
          ia[i] = byteString.charCodeAt(i);
      return new Blob([ab], { type: mimeString });
  }

  var pluginName = 'cropbox';

  function factory($) {
    function Crop($image, options) {
      this.width = null;
      this.height = null;
      this.img_width = null;
      this.img_height = null;
      this.img_left = 0;
      this.img_top = 0;
      this.minPercent = null;
      this.options = options;
      this.$image = $image;
      this.$image.hide().prop('draggable', false).addClass('cropImage').wrap('<div class="cropFrame" />'); // wrap image in frame;
      this.$frame = this.$image.parent();
      this.init(options.xml);
    }

    Crop.prototype = {
      init: function (xml) {
        var self = this;
        var defaultControls = $('<div/>', { 'class' : 'cropControls' })
              .append($('<span class="spnCroptoolDes">Drag to pan</span>'))//.append(' <button class="cropButton">Crop</button>')
              .append($('<a/>', { 'class' : 'cropZoomIn' }).on('click', $.proxy(this.zoomIn, this)))
              .append($('<a/>', { 'class' : 'cropZoomOut' }).on('click', $.proxy(this.zoomOut, this)));

        this.$frame.append(this.options.controls || defaultControls);
        this.updateOptions(xml);

        if (typeof $.fn.hammer === 'function' || typeof Hammer !== 'undefined') {
          var hammerit, dragData;
          if (typeof $.fn.hammer === 'function')
            hammerit = this.$image.hammer();
          else
            hammerit = Hammer(this.$image.get(0));

          hammerit.on('touch', function(e) {
            e.gesture.preventDefault();
          }).on("dragleft dragright dragup dragdown", function(e) { 
            if (!dragData)
              dragData = {
                startX: self.img_left,
                startY: self.img_top,
              };
            dragData.dx = e.gesture.deltaX;
            dragData.dy = e.gesture.deltaY;
            e.gesture.preventDefault();
            e.gesture.stopPropagation();
            self.drag.call(self, dragData, true);
          }).on('release', function(e) {
            e.gesture.preventDefault();
            dragData = null;
            self.update.call(self);
          }).on('doubletap', function(e) {
            e.gesture.preventDefault();
            self.zoomIn.call(self);
          }).on('pinchin', function (e) {
            e.gesture.preventDefault();
            self.zoomOut.call(self);
          }).on('pinchout', function (e) {
            e.gesture.preventDefault();
            self.zoomIn.call(self);
          });
          $('.imgOrignalCrop').on('mousedown', function (e1) {
              console.log("called");
              var dragData = {
                  startX: self.img_left,
                  startY: self.img_top,
              };
              e1.preventDefault();
              $(document).on('mousemove' , function (e2) {
                  dragData.dx = e2.pageX - e1.pageX;
                  dragData.dy = e2.pageY - e1.pageY;
                  self.drag.call(self, dragData, true);
              }).on('mouseup' , function () {
                  self.update.call(self);
                  $(document).off('mouseup' );
                  $(document).off('mousemove' );
              });
          });
        } else {
          this.$image.on('mousedown.' + pluginName, function(e1) {
            var dragData = {
              startX: self.img_left,
              startY: self.img_top,
            };
            e1.preventDefault();
            $(document).on('mousemove.' + pluginName, function (e2) {
              dragData.dx = e2.pageX - e1.pageX;
              dragData.dy = e2.pageY - e1.pageY;
              self.drag.call(self, dragData, true);
            }).on('mouseup.' + pluginName, function() {
              self.update.call(self);
              $(document).off('mouseup.' + pluginName);
              $(document).off('mousemove.' + pluginName);
            });
          });
        
        }
        if ($.fn.mousewheel) {
          this.$image.on('mousewheel.' + pluginName, function (e) {
            e.preventDefault();
            if (e.deltaY < 0)
              self.zoomIn.call(self);
            else
              self.zoomOut.call(self);
          });
        }
       
        croppedInstance = this;
        
      },

      updateOptions: function (xml) {
          StartLoader();
        var self = this;
        self.img_top = 0;
        self.img_left = 0;
        self.$image.css({width: '', left: self.img_left, top: self.img_top});
        self.$frame.width(self.options.width).height(self.options.height);
        self.$frame.off('.' + pluginName);
        self.$frame.removeClass('hover');
        if (self.options.showControls === 'always' || self.options.showControls === 'auto' && is_touch_device())
          self.$frame.addClass('hover');
        else if (self.options.showControls !== 'never') {
          self.$frame.on('mouseenter.' + pluginName, function () { self.$frame.addClass('hover'); });
          self.$frame.on('mouseleave.' + pluginName, function () { self.$frame.removeClass('hover'); });
        }

        // Image hack to get width and height on IE
        var img = new Image();
        img.src = self.$image.attr('src');
        img.onload = function () {
            StopLoader();
          self.width = img.width;
          self.height = img.height;
          ActualImHeight = img.height;
          ActualImWidth = img.width;
          
          img.src = '';
          img.onload = null;
          self.percent = undefined;
          self.fit.call(self);
          if (self.options.result)
            self.setCrop.call(self, self.options.result);
          else
            self.zoom.call(self, self.minPercent);
          self.$image.fadeIn('fast');
          if (xml != undefined && xml != null && xml != '') {
              var parser, xmlDoc;
              if (window.DOMParser) {
                  parser = new DOMParser();
                  xmlDoc = parser.parseFromString(xml, "text/xml");
              }
              else {
                  xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                  xmlDoc.async = false;
                  xmlDoc.loadXML(xml);
              }
              var percent = parseFloat(xmlDoc.getElementsByTagName("crv1")[0].childNodes[0].nodeValue);
              var img_width = parseInt(xmlDoc.getElementsByTagName("crv2")[0].childNodes[0].nodeValue);
              var sx = parseInt(xmlDoc.getElementsByTagName("sx")[0].childNodes[0].nodeValue);
              var sy = parseInt(xmlDoc.getElementsByTagName("sy")[0].childNodes[0].nodeValue);
              var img_height = parseInt(xmlDoc.getElementsByTagName("crv3")[0].childNodes[0].nodeValue);
              var img_left = parseInt(xmlDoc.getElementsByTagName("crv4")[0].childNodes[0].nodeValue);
              var img_top = parseInt(xmlDoc.getElementsByTagName("crv5")[0].childNodes[0].nodeValue); 
              if ($(".cropFrame").width()> 170) {
                  $(".spnCroptoolDes").css("display", "");
              } else {
                  $(".spnCroptoolDes").css("display", "none");
              }
              self.setCropFirst({ percent: parseFloat(percent), img_width: parseFloat(img_width), img_height: parseFloat(img_height), img_left: parseFloat(img_left), img_top: parseFloat(img_top), sx: sx, sy: sy });
              pcL20_new_MoveImg(self.$image.attr('src'), percent, ActualImHeight, ActualImWidth);
              console.log(percent + " " + ActualImHeight + " " + ActualImWidth);
          }
        };
      },

      remove: function () {
        var hammerit;
        if (typeof $.fn.hammer === 'function')
          hammerit = this.$image.hammer();
        else if (typeof Hammer !== 'undefined')
          hammerit = Hammer(this.$image.get(0));
        if (hammerit)
          hammerit.off('mousedown dragleft dragright dragup dragdown release doubletap pinchin pinchout');
        this.$frame.off('.' + pluginName);
        this.$image.off('.' + pluginName);
        this.$image.css({width: '', left: '', top: ''});
        this.$image.removeClass('cropImage');
        this.$image.removeData('cropbox');
        this.$image.insertAfter(this.$frame);
        this.$frame.removeClass('cropFrame');
        this.$frame.removeAttr('style');
        this.$frame.empty();
        this.$frame.hide();
      },

      fit: function () {
        var widthRatio = this.options.width / this.width,
          heightRatio = this.options.height / this.height;
        this.minPercent = (widthRatio >= heightRatio) ? widthRatio : heightRatio;
      },

      setCrop: function (result) {
        this.percent = Math.max(this.options.width/result.cropW, this.options.height/result.cropH);
        this.img_width = Math.ceil(this.width*this.percent);
        this.img_height = Math.ceil(this.height*this.percent);
        this.img_left = -Math.floor(result.cropX*this.percent);
        this.img_top = -Math.floor(result.cropY*this.percent);
        this.$image.css({ width: this.img_width, left: this.img_left, top: this.img_top });
        this.update();
      },
      setCropFirst: function (result) {
          this.percent = result.percent;
          this.img_width =result.img_width;
          this.img_height = result.img_height;
          this.img_left = result.img_left;
          this.img_top = result.img_top;
          this.$image.css({ width: this.img_width, left: this.img_left, top: this.img_top });
          this.update();
         // this.drag({ startX: 0, startY: 0, dx: result.sx, dy: result.sy }, true);
          //this.update();
      },
      zoom: function(percent) {
        var old_percent = this.percent;

        this.percent = Math.max(this.minPercent, Math.min(this.options.maxZoom, percent));
        this.img_width = Math.ceil(this.width * this.percent);
        this.img_height = Math.ceil(this.height * this.percent);

        if (old_percent) {
          var zoomFactor = this.percent / old_percent;
          this.img_left = fill((1 - zoomFactor) * this.options.width / 2 + zoomFactor * this.img_left, this.img_width, this.options.width);
          this.img_top = fill((1 - zoomFactor) * this.options.height / 2 + zoomFactor * this.img_top, this.img_height, this.options.height);
        } else {
          this.img_left = fill((this.options.width - this.img_width) / 2, this.img_width,  this.options.width);
          this.img_top = fill((this.options.height - this.img_height) / 2, this.img_height, this.options.height);
        }

        this.$image.css({ width: this.img_width, left: this.img_left, top: this.img_top });
        this.update();
      },
      zoomIn: function() {
        this.zoom(this.percent + (1 - this.minPercent) / (this.options.zoom - 1 || 1));
      },
      zoomOut: function() {
        this.zoom(this.percent - (1 - this.minPercent) / (this.options.zoom - 1 || 1));
      },
      drag: function(data, skipupdate) {
        this.img_left = fill(data.startX + data.dx, this.img_width, this.options.width);
        this.img_top = fill(data.startY + data.dy, this.img_height, this.options.height);
        this.$image.css({ left: this.img_left, top: this.img_top });
        if (skipupdate)
          this.update();
      },
      update: function() {
        this.result = {
          cropX: -Math.ceil(this.img_left / this.percent),
          cropY: -Math.ceil(this.img_top / this.percent),
          cropW: Math.floor(this.options.width / this.percent),
          cropH: Math.floor(this.options.height / this.percent),
          crv1: this.percent,
          crv2: this.img_width,
          crv3: this.img_height,
          crv4: this.img_left,
          crv5: this.img_top,
          crv6: ActualImHeight,  // only used for GUI Changes not stored
          crv7: ActualImWidth,// only used for GUI Changes not stored
          stretch: this.minPercent > 1
        };

        this.$image.trigger(pluginName, [this.result, this]);
      },
      getDataURL: function () {
        var canvas = document.createElement('canvas'), ctx = canvas.getContext('2d');
        canvas.width = this.options.width;
        canvas.height = this.options.height;
        ctx.drawImage(this.$image.get(0), this.result.cropX, this.result.cropY, this.result.cropW, this.result.cropH, 0, 0, this.options.width, this.options.height);
        return canvas.toDataURL();
      },
      getBlob: function () {
        return uri2blob(this.getDataURL());
      },
    };

    $.fn[pluginName] = function(options) {
      return this.each(function() {
        var inst = $.data(this, pluginName);
        if (!inst) {
          var opts = $.extend({}, $.fn[pluginName].defaultOptions, options);
          $.data(this, pluginName, new Crop($(this), opts));
        } else if (options) {
          $.extend(inst.options, options);
          inst.updateOptions();
        }
      });
    };

    $.fn[pluginName].defaultOptions = {
      width: 200,
      height: 200,
      zoom: 10,
      maxZoom: 1,
      controls: null,
      showControls: 'auto'
    };
  }

  if (typeof require === "function" && typeof exports === "object" && typeof module === "object")
      factory(require("jquery"));
  else if (typeof define === "function" && define.amd)
      define(["jquery"], factory);
  else
      factory(window.jQuery || window.Zepto);

})();
