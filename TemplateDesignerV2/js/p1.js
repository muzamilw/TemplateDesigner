

$.widget('oal.fontSelector', {
    options: {
        inSpeed: 250,
        outSpeed: 150,
        bold: false,
        italic: false,
        underline: false
    },
    _create: function () {
        var font, fontEl, fontLabel, fontName, fonts, label, _i, _j, _len, _len2, _ref, _ref2,
          _this = this;
        this.element.hide();
        fonts = [];
        _ref = this.element.children();
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            font = _ref[_i];
            fontLabel = $(font).text();
            fontName = $(font).attr('value');
            if ($(font).attr('selected')) this.selected = fontName;
            fonts.push([fontName, fontLabel]);
        }
        if (!this.selected) this.selected = fonts[0][0];
        this.dropdown = $('<div class="fontSelector ui-widget"><span class="handle">&#9660;</span></div>');
        this.list = $('<ul class="fonts"></ul>');
        for (_j = 0, _len2 = fonts.length; _j < _len2; _j++) {
            font = fonts[_j];
            _ref2 = font, font = _ref2[0], label = _ref2[1];
         //   this.element.before("<link rel='stylesheet' type='text/css' href='http://fonts.googleapis.com/css?family=" + font + ":400,700,400italic,700italic'></link>");
            fontEl = $("<li style=\"font-family: '" + font + "'\">" + label + "</li>");
            fontEl.data('font', font);
            if (font === this.selected) {
                fontEl.addClass('selected');
                this.dropdown.prepend($("<h4 style=\"font-family: '" + font + "'\" class='selected handle'>" + label + "</h4>"));
            }
            this.list.append(fontEl);
        }
        
        this.dropdown.append(this.list);
        this.element.after(this.dropdown);
        return $('div.fontSelector .handle').click(function () {
            return _this._toggleOpen();
        });
    },
    _toggleOpen: function () {
        var _this = this;
        if (this.dropdown.attr("disabled") == "disabled") return;
        if (this.list.is(':visible')) {
            this.dropdown.find('span.handle').html('&#9660;');
            return this.list.slideUp(this.options.outSpeed);
        } else {
            this.dropdown.find('span.handle').html('&#9650;');
            this.list.slideDown(this.options.inSpeed);
            return $('div.fontSelector ul.fonts li').click(function (e) {
                var font, fontLi, fontName, fontOption, label, oldFont, _i, _j, _len, _len2, _ref, _ref2, _results;
                font = $(e.target).data('font');
                label = $(e.target).text();
                oldFont = _this.selected;
                if (font === oldFont) return false;
                _this._trigger('fontChange', null, {
                    font: font,
                    oldFont: oldFont
                });
                _this.selected = font;
                $('div.fontSelector h4.selected').text(label).css({
                    fontFamily: font
                });
                _ref = _this.list.children();
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    fontLi = _ref[_i];
                    if ($(fontLi).data('font') === font) {
                        $(fontLi).addClass('selected');
                    } else if ($(fontLi).data('font') === oldFont) {
                        $(fontLi).removeClass('selected');
                    }
                }
                _ref2 = _this.element.children();
                _results = [];
                for (_j = 0, _len2 = _ref2.length; _j < _len2; _j++) {
                    fontOption = _ref2[_j];
                    fontName = $(fontOption).val();
                    if (fontName === font) {
                        _results.push($(fontOption).attr('selected', 'selected'));
                    } else {
                        _results.push(void 0);
                    }
                }
                
                _this._setOption("close", "true");
                pcL04(1);
                return _results;
            });
        }
    },
    _setOption: function (key, value) {
        if (key === 'bold' && (value === true || value === false)) {
            this.options.bold = value;
            if (value === true) {
                this.dropdown.find('h4.selected').css({
                    fontWeight: 'bold'
                });
                this.list.css({
                    fontWeight: 'bold'
                });
            } else {
                this.dropdown.find('h4.selected').css({
                    fontWeight: 'normal'
                });
                this.list.css({
                    fontWeight: 'normal'
                });
            }
        } else if (key === 'italic' && (value === true || value === false)) {
            this.options.italic = value;
            if (value === true) {
                this.dropdown.find('h4.selected').css({
                    fontStyle: 'italic'
                });
                this.list.css({
                    fontStyle: 'italic'
                });
            } else {
                this.dropdown.find('h4.selected').css({
                    fontStyle: 'normal'
                });
                this.list.css({
                    fontStyle: 'normal'
                });
            }
        } else if (key === 'underline' && (value === true || value === false)) {
            this.options.underline = value;
            if (value === true) {
                this.dropdown.find('h4.selected').css({
                    textDecoration: 'underline'
                });
                this.list.css({
                    textDecoration: 'underline'
                });
            } else {
                this.dropdown.find('h4.selected').css({
                    textDecoration: 'none'
                });
                this.list.css({
                    textDecoration: 'none'
                });
            }
        } else if (key === 'font') {
            var font, fontLi, fontName, fontOption, label, oldFont, _i, _j, _len, _len2, _ref, _ref2, _results;
            font = value;
            label = value;
            oldFont = this.selected;
            if (font === oldFont) return false;
            this._trigger('fontChange', null, {
                font: font,
                oldFont: oldFont
            });
            this.selected = font;
            $('div.fontSelector h4.selected').text(label).css({
                fontFamily: font
            });
            _ref = this.list.children();
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                fontLi = _ref[_i];
                if ($(fontLi).data('font') === font) {
                    $(fontLi).addClass('selected');
                } else if ($(fontLi).data('font') === oldFont) {
                    $(fontLi).removeClass('selected');
                }
            }
            _ref2 = this.element.children();
            _results = [];
            for (_j = 0, _len2 = _ref2.length; _j < _len2; _j++) {
                fontOption = _ref2[_j];
                fontName = $(fontOption).val();
                if (fontName === font) {
                    _results.push($(fontOption).attr('selected', 'selected'));
                } else {
                    _results.push(void 0);
                }
            }
            return _results;
        } else if (key === 'close') {

            this.dropdown.find('span.handle').html('&#9660;');
            return this.list.slideUp(this.options.outSpeed);
        }
        if ((key === 'bold' || key === 'italic' || key === 'underline') && (value === true || value === false)) {
            return this._trigger('styleChange', null, {
                style: key,
                value: value
            });
        }
    },
    destroy: function () {
        return Widget.prototype.destroy.call(this);
    }
});
