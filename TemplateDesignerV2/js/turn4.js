﻿/* turn.js r4 | Copyright (c) 2012 Emmanuel Garcia | turnjs.com | turnjs.com/legal.txt */
(function (g) {
    function O(a, b, c) {
        if (!c[0] || "object" == typeof c[0])
            return b.init.apply(a, c);
        if (b[c[0]])
            return b[c[0]].apply(a, Array.prototype.slice.call(c, 1));
        throw n(c[0] + " is not a method");
    }
    function m(a, b, c, d) {
        return {
            css: {
                position: "absolute",
                top: a,
                left: b,
                overflow: d || "hidden",
                zIndex: c || "auto"
            }
        }
    }
    function P(a, b, c, d, e) {
        var f = 1 - e, Q = f * f * f, g = e * e * e;
        return j(Math.round(Q * a.x + 3 * e * f * f * b.x + 3 * e * e * f * c.x + g * d.x), Math.round(Q * a.y + 3 * e * f * f * b.y + 3 * e * e * f * c.y + g * d.y))
    }
    function j(a, b) {
        return {
            x: a,
            y: b
        }
    }
    function p(a, b, c) {
        return A &&
        c ? " translate3d(" + a + "px," + b + "px, 0px) " : " translate(" + a + "px, " + b + "px) "
    }
    function q(a) {
        return " rotate(" + a + "deg) "
    }
    function o(a, b) {
        return Object.prototype.hasOwnProperty.call(b, a)
    }
    function R() {
        for (var a = ["Moz", "Webkit", "Khtml", "O", "ms"], b = a.length, c = ""; b--; )
            a[b] + "Transform" in document.body.style && (c = "-" + a[b].toLowerCase() + "-");
        return c
    }
    function M(a, b, c, d, e) {
        var f, g = [];
        if ("-webkit-" == x) {
            for (f = 0; f < e; f++)
                g.push("color-stop(" + d[f][0] + ", " + d[f][1] + ")");
            a.css({
                "background-image": "-webkit-gradient(linear, " +
                b.x + "% " + b.y + "%," + c.x + "% " + c.y + "%, " + g.join(",") + " )"
            })
        }
        else {
            var b = {
                x: b.x / 100 * a.width(),
                y: b.y / 100 * a.height()
            },
            c = {
                x: c.x / 100 * a.width(),
                y: c.y / 100 * a.height()
            },
            i = c.x - b.x;
            f = c.y - b.y;
            var h = Math.atan2(f, i), r = h - Math.PI / 2, r = Math.abs(a.width() * Math.sin(r)) + Math.abs(a.height() * Math.cos(r)), i = Math.sqrt(f * f + i * i), c = j(c.x < b.x ? a.width() : 0, c.y < b.y ? a.height() : 0), k = Math.tan(h);
            f = -1 / k;
            k = (f * c.x - c.y - k * b.x + b.y) / (f - k);
            c = f * k - f * c.x + c.y;
            b = Math.sqrt(Math.pow(k - b.x, 2) + Math.pow(c - b.y, 2));
            for (f = 0; f < e; f++)
                g.push(" " + d[f][1] + " " + 100 *
                (b + i * d[f][0]) / r + "%");
            a.css({
                "background-image": x + "linear-gradient(" + -h + "rad," + g.join(",") + ")"
            })
        }
    }
    function n(a) {
        function b(a) {
            this.name = "TurnJsError";
            this.message = a
        }
        b.prototype = Error();
        b.prototype.constructor = b;
        return new b(a)
    }
    function B(a) {
        var b = {
            top: 0,
            left: 0
        };
        do
            b.left += a.offsetLeft, b.top += a.offsetTop;
        while (a = a.offsetParent);
        return b
    }
    var A, x = "", J = Math.PI, G = J / 2, u = "ontouchstart" in window, s = u ? {
        down: "touchstart",
        move: "touchmove",
        up: "touchend",
        over: "touchstart",
        out: "touchend"
    } : {
        down: "mousedown",
        move: "mousemove",
        up: "mouseup",
        over: "mouseover",
        out: "mouseout"
    },
    S = {
        backward: ["bl", "tl", "l"],
        forward: ["br", "tr", "r"],
        all: "tl,bl,tr,br,l,r".split(",")
    },
    T = ["single", "double"], U = {
        acceleration: !0,
        display: "double",
        duration: 600,
        page: 1,
        gradients: !0,
        when: null
    },
    V = {
        acceleration: !0,
        corners: "forward",
        cornerSize: 100,
        duration: 600,
        gradients: !0
    },
    h = {
        init: function (a) {
            A = "WebKitCSSMatrix" in window || "MozPerspective" in document.body.style;
            x = R();
            var b, c = this, d = 0, e = this.data(), f = this.children(), a = g.extend({
                width: this.width(),
                height: this.height()
            },
            U, a);
            e.opts = a;
            e.pageObjs = {};
            e.pages = {};
            e.pageWrap = {};
            e.pagePlace = {};
            e.pageMv = [];
            e.zoom = 1;
            e.totalPages = a.pages || 0;
            e.eventHandlers = {
                mouseStart: function () {
                    for (var a in e.pages)
                        if (o(a, e.pages) && !1 === i._eventStart.apply(e.pages[a], arguments))
                            return !1
                    },
                    mouseMove: function () {
                        for (var a in e.pages)
                            o(a, e.pages) && i._eventMove.apply(e.pages[a], arguments)
                    },
                    mouseEnd: function () {
                        for (var a in e.pages)
                            o(a, e.pages) && i._eventEnd.apply(e.pages[a], arguments)
                    },
                    start: function () {
                        return h._start.apply(c, arguments)
                    }
                };
                if (a.when)
                    for (b in a.when)
                        o(b,
                    a.when) && this.bind(b, a.when[b]);
                this.css({
                    position: "relative",
                    width: a.width,
                    height: a.height
                });
                this.turn("display", a.display);
                A && !u && a.acceleration && this.transform(p(0, 0, !0));
                for (b = 0; b < f.length; b++)
                    "1" != g(f[b]).attr("ignore") && this.turn("addPage", f[b], ++d);
                g(this).bind(s.down, e.eventHandlers.mouseStart).bind("end", h._end).bind("pressed", h._pressed).bind("released", h._released).bind("flip", h._flip);
                g(this).parent().bind("start", e.eventHandlers.start);
                g(document).bind(s.move, e.eventHandlers.mouseMove).bind(s.up,
            e.eventHandlers.mouseEnd);
                this.turn("page", a.page);
                e.done = !0;
                return this
            },
            addPage: function (a, b) {
                var c, d = !1, e = this.data(), f = e.totalPages + 1;
                if (e.destroying)
                    return !1;
                if (c = /\bp([0-9]+)\b/.exec(g(a).attr("class")))
                    b = parseInt(c[1], 10);
                if (b)
                    if (b == f)
                        d = !0;
                    else {
                        if (b > f)
                            throw n('Page "' + b + '" cannot be inserted');
                    }
                else
                    b = f, d = !0;
                1 <= b && b <= f && (c = "double" == e.display ? b % 2 ? " odd" : " even" : "", e.done && this.turn("stop"), b in e.pageObjs && h._movePages.call(this, b, 1), d && (e.totalPages = f), e.pageObjs[b] = g(a).css({
                    "float": "left"
                }).addClass("page p" +
            b + c), h._addPage.call(this, b), h._removeFromDOM.call(this));
                return this
            },
            _addPage: function (a) {
                var b = this.data(), c = b.pageObjs[a];
                if (c)
                    if (h._necessPage.call(this, a)) {
                        if (!b.pageWrap[a]) {
                            var d = h._pageSize.call(this, a, !0);
                            c.css({
                                width: d.width,
                                height: d.height
                            });
                            b.pagePlace[a] = a;
                            b.pageWrap[a] = g("<div/>", {
                                "class": "page-wrapper",
                                page: a,
                                css: {
                                    position: "absolute",
                                    overflow: "hidden"
                                }
                            }).css(d);
                            this.append(b.pageWrap[a]);
                            b.pageObjs[a].appendTo(b.pageWrap[a])
                        }
                        h._makeFlip.call(this, a)
                    }
                    else
                        b.pagePlace[a] = 0, b.pageObjs[a] &&
                b.pageObjs[a].remove()
            },
            hasPage: function (a) {
                return o(a, this.data().pageObjs)
            },
            center: function (a) {
                var b = this.data(), c = g(this).turn("size"), d = c.width / (2 * b.zoom) - c.width / 2;
                b.noCenter || ("double" == b.display && (a = this.turn("view", a || b.tpage || b.page), a[0] ? a[1] || (d += c.width / 4) : d -= c.width / 4), g(this).css({
                    marginLeft: d
                }));
                return this
            },
            destroy: function () {
                var a = this, b = this.data();
                b.destroying = !0;
                g.each("end,first,flip,last,pressed,released,start,turning,turned,zooming,missing".split(","), function (b, d) {
                    a.unbind(d)
                });
                this.parent().unbind("start", b.eventHandlers.start);
                for (g(document).unbind(s.move, b.eventHandlers.mouseMove).unbind(s.up, b.eventHandlers.mouseEnd); 0 !== b.totalPages; )
                    this.turn("removePage", b.totalPages);
                b.fparent && b.fparent.remove();
                b.shadow && b.shadow.remove();
                this.removeData();
                b = null;
                return this
            },
            is: function () {
                return "object" == typeof this.data().pages
            },
            zoom: function (a) {
                var b = this.data();
                if ("number" == typeof a) {
                    if (0.0010 > a || 100 < a)
                        throw n(a + " is not a value for zoom");
                    var c = g.Event("zooming");
                    this.trigger(c,
                [a, b.zoom]);
                    if (c.isDefaultPrevented())
                        return this;
                    var c = g(this).turn("size"), d = 1 / b.zoom, e = Math.round(c.width * d * a), f = Math.round(c.height * d * a);
                    b.zoom = a;
                    g(this).turn("stop").turn("size", e, f).css({
                        marginTop: c.height * d / 2 - f / 2
                    });
                    b.opts.autoCenter ? this.turn("center") : g(this).css({
                        marginLeft: c.width * d / 2 - e / 2
                    });
                    h._updateShadow.call(this);
                    return this
                }
                return b.zoom
            },
            _pageSize: function (a, b) {
                var c = this.data(), d = {};
                if ("single" == c.display)
                    d.width = this.width(), d.height = this.height(), b && (d.top = 0, d.left = 0, d.right = "auto");
                else {
                    var e = this.width() / 2, f = this.height();
                    c.pageObjs[a].hasClass("own-size") ? (d.width = c.pageObjs[a].width(), d.height = c.pageObjs[a].height()) : (d.width = e, d.height = f);
                    b && (c = a % 2, d.top = (f - d.height) / 2, d[c ? "right" : "left"] = e - d.width, d[c ? "left" : "right"] = "auto")
                }
                return d
            },
            _makeFlip: function (a) {
                var b = this.data();
                if (!b.pages[a] && b.pagePlace[a] == a) {
                    var c, d = "single" == b.display, e = a % 2;
                    b.pageObjs[a].hasClass("hard") ? c = e ? "r" : "l" : (b.pageObjs[a].addClass("sheet"), c = d ? "all" : e ? "forward" : "backward");
                    b.pages[a] = b.pageObjs[a].css(h._pageSize.call(this,
                a)).flip({
                    page: a,
                    next: e || d ? a + 1 : a - 1,
                    turn: this,
                    duration: b.opts.duration,
                    acceleration: b.opts.acceleration,
                    corners: c,
                    gradients: b.opts.gradients
                }).flip("disable", b.disabled)
                }
                return b.pages[a]
            },
            _makeRange: function () {
                var a, b;
                if (!(1 > this.data().totalPages)) {
                    b = this.turn("range");
                    for (a = b[0]; a <= b[1]; a++)
                        h._addPage.call(this, a)
                }
            },
            range: function (a) {
                var b, c, d, e = this.data(), a = a || e.tpage || e.page || 1;
                d = h._view.call(this, a);
                if (1 > a || a > e.totalPages)
                    throw n('"' + a + '" is not a page for range');
                d[1] = d[1] || d[0];
                1 <= d[0] && d[1] <=
            e.totalPages ? (a = Math.floor(2), e.totalPages - d[1] > d[0] ? (b = Math.min(d[0] - 1, a), c = 2 * a - b) : (c = Math.min(e.totalPages - d[1], a), b = 2 * a - c)) : c = b = 5;
                return [Math.max(1, d[0] - b), Math.min(e.totalPages, d[1] + c)]
            },
            _necessPage: function (a) {
                if (0 === a)
                    return !0;
                var b = this.turn("range");
                return this.data().pageObjs[a].hasClass("fixed") || a >= b[0] && a <= b[1]
            },
            _removeFromDOM: function () {
                var a, b = this.data();
                for (a in b.pageWrap)
                    o(a, b.pageWrap) && !h._necessPage.call(this, a) && h._removePageFromDOM.call(this, a)
            },
            _removePageFromDOM: function (a) {
                var b =
            this.data();
                if (b.pages[a]) {
                    var c = b.pages[a].data();
                    i._moveFoldingPage.call(b.pages[a], !1);
                    c.f && c.f.fwrapper && c.f.fwrapper.remove();
                    b.pages[a].removeData();
                    b.pages[a].remove();
                    delete b.pages[a]
                }
                b.pageObjs[a] && b.pageObjs[a].remove();
                b.pageWrap[a] && (b.pageWrap[a].remove(), delete b.pageWrap[a]);
                h._removeMv.call(this, a);
                delete b.pagePlace[a]
            },
            removePage: function (a) {
                var b = this.data();
                if (1 > a || a > b.totalPages)
                    throw n("The page " + a + " doesn't exist");
                b.pageObjs[a] && (this.turn("stop"), h._removePageFromDOM.call(this,
            a), delete b.pageObjs[a]);
                h._movePages.call(this, a, -1);
                b.totalPages -= 1;
                b.page > b.totalPages ? this.turn("page", b.totalPages) : h._makeRange.call(this);
                return this
            },
            _movePages: function (a, b) {
                var c, d = this, e = this.data(), f = "single" == e.display, g = function (a) {
                    var c = a + b, g = c % 2, i = g ? " odd " : " even ";
                    e.pageObjs[a] && (e.pageObjs[c] = e.pageObjs[a].removeClass("p" + a + " odd even").addClass("p" + c + i));
                    e.pagePlace[a] && e.pageWrap[a] && (e.pagePlace[c] = c, e.pageWrap[c] = e.pageObjs[c].hasClass("fixed") ? e.pageWrap[a].attr("page", c) :
                e.pageWrap[a].css(h._pageSize.call(d, c, !0)).attr("page", c), e.pages[a] && (e.pages[c] = e.pages[a].flip("options", {
                    page: c,
                    next: f || g ? c + 1 : c - 1,
                    corners: f ? "all" : g ? "forward" : "backward"
                })), b && (delete e.pages[a], delete e.pagePlace[a], delete e.pageObjs[a], delete e.pageWrap[a], delete e.pageObjs[a]))
                };
                if (0 < b)
                    for (c = e.totalPages; c >= a; c--)
                        g(c);
                else
                    for (c = a; c <= e.totalPages; c++)
                        g(c)
            },
            display: function (a) {
                var b = this.data(), c = b.display;
                if (a) {
                    if (-1 == g.inArray(a, T))
                        throw n('"' + a + '" is not a value for display');
                    "single" == a ?
                b.pageObjs[0] || (this.turn("stop").css({
                    overflow: "hidden"
                }), b.pageObjs[0] = g("<div />", {
                    "class": "page p-temporal"
                }).css({
                    width: this.width(),
                    height: this.height()
                }).appendTo(this)) : b.pageObjs[0] && (this.turn("stop").css({
                    overflow: ""
                }), b.pageObjs[0].remove(), delete b.pageObjs[0]);
                    b.display = a;
                    c && (a = this.turn("size"), h._movePages.call(this, 1, 0), this.turn("size", a.width, a.height).turn("update"));
                    return this
                }
                return c
            },
            animating: function () {
                return 0 < this.data().pageMv.length
            },
            corner: function () {
                var a, b, c = this.data();
                for (b in c.pages)
                    if (o(b, c.pages) && (a = c.pages[b].flip("corner")))
                        return a;
                return !1
            },
            disable: function (a) {
                var b, c = this.data(), d = this.turn("view");
                c.disabled = void 0 === a || !0 === a;
                for (b in c.pages)
                    o(b, c.pages) && c.pages[b].flip("disable", c.disabled ? !0 : -1 == g.inArray(parseInt(b, 10), d));
                return this
            },
            size: function (a, b) {
                if (a && b) {
                    var c, d, e = this.data();
                    d = "double" == e.display ? a / 2 : a;
                    this.css({
                        width: a,
                        height: b
                    });
                    e.pageObjs[0] && e.pageObjs[0].css({
                        width: d,
                        height: b
                    });
                    for (c in e.pageWrap)
                        o(c, e.pageWrap) && (d = h._pageSize.call(this,
                    c, !0), e.pageObjs[c].css({
                        width: d.width,
                        height: d.height
                    }), e.pageWrap[c].css(d), e.pages[c] && e.pages[c].css({
                        width: d.width,
                        height: d.height
                    }));
                    this.turn("resize");
                    return this
                }
                return {
                    width: this.width(),
                    height: this.height()
                }
            },
            resize: function () {
                var a, b = this.data();
                b.pages[0] && (b.pageWrap[0].css({
                    left: -this.width()
                }), b.pages[0].flip("resize", !0));
                for (a = 1; a <= b.totalPages; a++)
                    b.pages[a] && b.pages[a].flip("resize", !0)
            },
            _removeMv: function (a) {
                var b, c = this.data();
                for (b = 0; b < c.pageMv.length; b++)
                    if (c.pageMv[b] == a)
                        return c.pageMv.splice(b,
                    1), !0;
                return !1
            },
            _addMv: function (a) {
                var b = this.data();
                h._removeMv.call(this, a);
                b.pageMv.push(a)
            },
            _view: function (a) {
                var b = this.data(), a = a || b.page;
                return "double" == b.display ? a % 2 ? [a - 1, a] : [a, a + 1] : [a]
            },
            view: function (a) {
                var b = this.data(), a = h._view.call(this, a);
                return "double" == b.display ? [0 < a[0] ? a[0] : 0, a[1] <= b.totalPages ? a[1] : 0] : [0 < a[0] && a[0] <= b.totalPages ? a[0] : 0]
            },
            stop: function (a, b) {
                if (this.turn("animating")) {
                    var c, d, e, f = this.data();
                    f.tpage && (f.page = f.tpage, delete f.tpage);
                    for (c = 0; c < f.pageMv.length; c++)
                        f.pageMv[c] &&
                    f.pageMv[c] !== a && (e = f.pages[f.pageMv[c]], d = e.data().f.opts, e.flip("hideFoldedPage", b), b || i._moveFoldingPage.call(e, !1), d.force && (d.next = 0 === d.page % 2 ? d.page - 1 : d.page + 1, delete d.force))
                }
                this.turn("update");
                return this
            },
            pages: function (a) {
                var b = this.data();
                if (a) {
                    if (a < b.totalPages) {
                        for (var c = a + 1; c <= b.totalPages; c++)
                            this.turn("removePage", c);
                        this.turn("page") > a && this.turn("page", a)
                    }
                    b.totalPages = a;
                    return this
                }
                return b.totalPages
            },
            _missing: function (a) {
                for (var b = this.data(), c = this.turn("range", a), d = [], a = c[0]; a <=
            c[1];
            a++) b.pageObjs[a] || d.push(a);
                0 < d.length && this.trigger("missing", [d])
            },
            _fitPage: function (a) {
                var b = this.data(), c = this.turn("view", a);
                h._missing.call(this, a);
                b.pageObjs[a] && (b.page = a, this.turn("stop"), h._removeFromDOM.call(this), h._makeRange.call(this), h._updateShadow.call(this), this.trigger("turned", [a, c]), b.opts.autoCenter && this.turn("center"))
            },
            _turnPage: function (a, b) {
                var c, d, e = this.data(), f = e.pagePlace[a], i = this.turn("view"), j = this.turn("view", a);
                if (e.page != a) {
                    var H = e.page, r = g.Event("turning");
                    this.trigger(r, [a, j]);
                    if (r.isDefaultPrevented()) {
                        H == e.page && -1 != g.inArray(f, e.pageMv) && e.pages[f].flip("hideFoldedPage", !0);
                        return
                    }
-1 != g.inArray(1, j) && this.trigger("first");
-1 != g.inArray(e.totalPages, j) && this.trigger("last")
                }
                if (b)
                    this.turn("stop", f);
                else {
                    h._missing.call(this, a);
                    if (!e.pageObjs[a])
                        return;
                    this.turn("stop");
                    e.page = a
                }
                h._makeRange.call(this);
                "single" == e.display ? (c = i[0], d = j[0]) : i[1] && a > i[1] ? (c = i[1], d = j[0]) : i[0] && a < i[0] && (c = i[0], d = j[1]);
                e.pages[c] && (f = e.pages[c].data().f.opts, e.tpage = d, f.next !=
            d && (f.next = d, f.force = !0), this.turn("update"), "single" == e.display ? e.pages[c].flip("turnPage", j[0] > i[0] ? "br" : "bl") : e.pages[c].flip("turnPage"))
            },
            page: function (a) {
                var b = this.data();
                if (void 0 === a)
                    return b.page;
                if (!b.disabled && !b.destroying) {
                    a = parseInt(a, 10);
                    if (0 < a && a <= b.totalPages)
                        return a != b.page && (!b.done || -1 != g.inArray(a, this.turn("view")) ? h._fitPage.call(this, a) : h._turnPage.call(this, a)), this;
                    throw n("The page " + a + " doesn't exist");
                }
            },
            next: function () {
                return this.turn("page", Math.min(this.data().totalPages,
            h._view.call(this, this.data().page).pop() + 1))
            },
            previous: function () {
                return this.turn("page", Math.max(1, h._view.call(this, this.data().page).shift() - 1))
            },
            peel: function (a, b) {
                var c = this.data(), d = this.turn("view"), b = "undefined" == typeof b ? !0 : !0 === b;
                !1 === a ? this.turn("stop", null, b) : "single" != c.display && (d = -1 != a.indexOf("l") ? d[0] : d[1], a.indexOf("t"), c.pages[d] && c.pages[d].flip("peel", a, b));
                return this
            },
            _addMotionPage: function () {
                var a = g(this).data().f.opts, b = a.turn;
                b.data();
                h._addMv.call(b, a.page)
            },
            _start: function (a,
        b, c) {
                var d = b.turn.data();
                a.isDefaultPrevented() || ("single" == d.display && (c = "l" == c.charAt(1), 1 == b.page && c || b.page == d.pages && !c ? a.preventDefault() : c ? (b.next = b.next < b.page ? b.next : b.page - 1, b.force = !0) : b.next = b.next > b.page ? b.next : b.page + 1), h._addMotionPage.call(a.target));
                h._updateShadow.call(b.turn)
            },
            _end: function (a, b, c) {
                g(a.target).data();
                var a = b.turn, d = a.data();
                if (c) {
                    if (c = d.tpage || d.page, c == b.next || c == b.page)
                        delete d.tpage, h._fitPage.call(a, c || b.next, !0)
                }
                else
                    h._removeMv.call(a, b.page), h._updateShadow.call(a),
                a.turn("update")
            },
            _pressed: function (a) {
                a.stopPropagation();
                var a = g(a.target).data().f, b = a.opts.turn;
                b.data().mouseAction = !0;
                b.turn("update");
                return a.time = (new Date).getTime()
            },
            _released: function (a, b) {
                a.stopPropagation();
                var c = g(a.target), d = c.data().f, e = d.opts.turn;
                e.data().mouseAction = !1;
                if (200 > (new Date).getTime() - d.time || 0 > b.x || b.x > c.width())
                    a.preventDefault(), h._turnPage.call(e, d.opts.next, !1 === i._cornerActivated.call(c, b, 1))
            },
            _flip: function (a) {
                a.stopPropagation();
                a = g(a.target).data().f.opts;
                a.turn.trigger("turn", [a.next]);
                a.turn.data().opts.autoCenter && a.turn.turn("center", a.next)
            },
            calculateZ: function (a) {
                var b, c, d, e, f = this, g = this.data();
                b = this.turn("view");
                var i = b[0] || b[1], h = a.length - 1, j = {
                    pageZ: {}, partZ: {}, pageV: {}
                },
            k = function (a) {
                a = f.turn("view", a);
                a[0] && (j.pageV[a[0]] = !0);
                a[1] && (j.pageV[a[1]] = !0)
            };
                for (b = 0; b <= h; b++)
                    c = a[b], d = g.pages[c].data().f.opts.next, e = g.pagePlace[c], k(c), k(d), c = g.pagePlace[d] == d ? d : c, j.pageZ[c] = g.totalPages - Math.abs(i - c), j.partZ[e] = 2 * g.totalPages - h + b;
                return j
            },
            update: function () {
                var a,
            b = this.data();
                if (this.turn("animating") && 0 !== b.pageMv[0]) {
                    var c, d = this.turn("calculateZ", b.pageMv), e = this.turn("corner"), f = this.turn("view"), i = this.turn("view", b.tpage);
                    for (a in b.pageWrap)
                        if (o(a, b.pageWrap) && (c = b.pageObjs[a].hasClass("fixed"), b.pageWrap[a].css({
                            display: d.pageV[a] || c ? "" : "none",
                            zIndex: (b.pageObjs[a].hasClass("hard") ? d.partZ[a] : d.pageZ[a]) || (c ? -1 : 0)
                        }), c = b.pages[a])) c.flip("z", d.partZ[a] || null), d.pageV[a] && c.flip("resize"), b.tpage ? c.flip("hover", !1).flip("disable", -1 == g.inArray(parseInt(a,
                10), b.pageMv) && a != i[0] && a != i[1]) : c.flip("hover", !1 === e).flip("disable", a != f[0] && a != f[1])
                }
                else
                    for (a in b.pageWrap)
                        o(a, b.pageWrap) && (d = h._setPageLoc.call(this, a), b.pages[a] && b.pages[a].flip("disable", b.disabled || 1 != d).flip("hover", !0).flip("z", null));
                return this
            },
            _updateShadow: function () {
                var a, b, c = this.data(), d = this.width(), e = this.height(), f = "single" == c.display ? d : d / 2;
                a = this.turn("view");
                c.shadow || (c.shadow = g("<div />", {
                    "class": "shadow",
                    css: m(0, 0, 0).css
                }).appendTo(this));
                for (var i = 0; i < c.pageMv.length &&
            a[0] && a[1];
            i++) {
                    a = this.turn("view", c.pages[c.pageMv[i]].data().f.opts.next);
                    b = this.turn("view", c.pageMv[i]);
                    a[0] = a[0] && b[0];
                    a[1] = a[1] && b[1]
                }
                a[0] ? a[1] ? c.shadow.css({
                    width: d,
                    height: e,
                    top: 0,
                    left: 0
                }) : c.shadow.css({
                    width: f,
                    height: e,
                    top: 0,
                    left: 0
                }) : c.shadow.css({
                    width: f,
                    height: e,
                    top: 0,
                    left: f
                })
            },
            _setPageLoc: function (a) {
                var b = this.data(), c = this.turn("view"), d = 0;
                if (a == c[0] || a == c[1])
                    d = 1;
                else if ("single" == b.display && a == c[0] + 1 || "double" == b.display && a == c[0] - 2 || a == c[1] + 2)
                    d = 2;
                if (!this.turn("animating"))
                    switch (d) {
                    case 1:
                        b.pageWrap[a].css({
                            zIndex: b.totalPages,
                            display: ""
                        });
                        break;
                    case 2:
                        b.pageWrap[a].css({
                            zIndex: b.totalPages - 1,
                            display: ""
                        });
                        break;
                    case 0:
                        b.pageWrap[a].css({
                            zIndex: 0,
                            display: b.pageObjs[a].hasClass("fixed") ? "" : "none"
                        })
                }
                return d
            }
        },
    i = {
        init: function (a) {
            this.data({
                f: {
                    disabled: !1,
                    hover: !0,
                    effect: "r" == a.corners || "l" == a.corners ? "hard" : "sheet"
                }
            });
            this.flip("options", a);
            i._addPageWrapper.call(this);
            return this
        },
        setData: function (a) {
            var b = this.data();
            b.f = g.extend(b.f, a);
            return this
        },
        options: function (a) {
            var b = this.data().f;
            return a ? (i.setData.call(this,
            {
                opts: g.extend({}, b.opts || V, a)
            }), this) : b.opts
        },
        z: function (a) {
            var b = this.data().f;
            b.opts["z-index"] = a;
            b.fwrapper && b.fwrapper.css({
                zIndex: a || parseInt(b.parent.css("z-index"), 10) || 0
            });
            return this
        },
        _cAllowed: function () {
            return S[this.data().f.opts.corners] || this.data().f.opts.corners
        },
        _cornerActivated: function (a) {
            var b = this.data().f, c = this.width(), d = this.height(), a = {
                x: a.x,
                y: a.y
            },
            e = b.opts.cornerSize;
            if (0 >= a.x || 0 >= a.y || a.x >= c || a.y >= d)
                return !1;
            var f = i._cAllowed.call(this);
            switch (b.effect) {
                case "hard":
                    if (a.x >
                    c - e) a.corner = "r";
                    else if (a.x < e)
                        a.corner = "l";
                    else
                        return !1;
                    break;
                case "sheet":
                    if (a.y < e)
                        a.corner = "t";
                    else if (a.y >= d - e)
                        a.corner = "b";
                    else
                        return !1;
                    if (a.x <= e)
                        a.corner += "l";
                    else if (a.x >= c - e)
                        a.corner += "r";
                    else
                        return !1
            }
            return -1 == g.inArray(a.corner, f) ? !1 : a
        },
        _isIArea: function (a) {
            var b = this.data().f.parent.offset(), a = u && a.originalEvent ? a.originalEvent.touches[0] : a;
            return i._cornerActivated.call(this, {
                x: a.pageX - b.left,
                y: a.pageY - b.top
            })
        },
        _c: function (a, b) {
            b = b || 0;
            switch (a) {
                case "tl":
                    return j(b, b);
                case "tr":
                    return j(this.width() -
                        b, b);
                case "bl":
                    return j(b, this.height() - b);
                case "br":
                    return j(this.width() - b, this.height() - b);
                case "l":
                    return j(b, 0);
                case "r":
                    return j(this.width() - b, 0)
            }
        },
        _c2: function (a) {
            switch (a) {
                case "tl":
                    return j(2 * this.width(), 0);
                case "tr":
                    return j(-this.width(), 0);
                case "bl":
                    return j(2 * this.width(), this.height());
                case "br":
                    return j(-this.width(), this.height());
                case "l":
                    return j(2 * this.width(), 0);
                case "r":
                    return j(-this.width(), 0)
            }
        },
        _foldingPage: function () {
            var a = this.data().f;
            if (a) {
                var b = a.opts;
                if (b.turn)
                    return a =
                        b.turn.data(), "single" == a.display ? a.pageObjs[b.next] ? a.pageObjs[0] : null : a.pageObjs[b.next]
            }
        },
        _backGradient: function () {
            var a = this.data().f, b = a.opts.turn;
            if ((b = a.opts.gradients && (!b || "single" == b.data().display || 2 != a.opts.page && a.opts.page != b.data().totalPages - 1)) && !a.bshadow)
                a.bshadow = g("<div/>", m(0, 0, 1)).css({
                    position: "",
                    width: this.width(),
                    height: this.height()
                }).appendTo(a.parent);
            return b
        },
        type: function () {
            return this.data().f.effect
        },
        resize: function (a) {
            var b = this.data().f, c = this.width(), d = this.height();
            switch (b.effect) {
                case "hard":
                    a && (b.wrapper.css({
                        width: c,
                        height: d
                    }), b.fpage.css({
                        width: c,
                        height: d
                    }), b.opts.gradients && (b.ashadow.css({
                        width: c,
                        height: d
                    }), b.bshadow.css({
                        width: c,
                        height: d
                    })));
                    break;
                case "sheet":
                    a && (a = Math.round(Math.sqrt(Math.pow(c, 2) + Math.pow(d, 2))), b.wrapper.css({
                        width: a,
                        height: a
                    }), b.fwrapper.css({
                        width: a,
                        height: a
                    }).children(":first-child").css({
                        width: c,
                        height: d
                    }), b.fpage.css({
                        width: d,
                        height: c
                    }), b.opts.gradients && b.ashadow.css({
                        width: d,
                        height: c
                    }), i._backGradient.call(this) && b.bshadow.css({
                        width: c,
                        height: d
                    })), b.parent.is(":visible") && (c = B(b.parent[0]), b.fwrapper.css({
                        top: c.top,
                        left: c.left
                    }), b.opts.turn && (c = B(b.opts.turn[0]), b.fparent.css({
                        top: -c.top,
                        left: -c.left
                    }))), this.flip("z", b.opts["z-index"])
            }
        },
        _addPageWrapper: function () {
            var a = this.data().f, b = this.parent();
            a.parent = b;
            if (!a.wrapper)
                switch (a.effect) {
                case "hard":
                    var c = {};
                    c[x + "transform-style"] = "preserve-3d";
                    c[x + "backface-visibility"] = "hidden";
                    a.wrapper = g("<div/>", m(0, 0, 2)).css(c).appendTo(b).prepend(this);
                    a.fpage = g("<div/>", m(0, 0, 1)).css(c).appendTo(b);
                    a.opts.gradients && (a.ashadow = g("<div/>", m(0, 0, 0)).hide().appendTo(b), a.bshadow = g("<div/>", m(0, 0, 0)));
                    break;
                case "sheet":
                    var c = this.width(), d = this.height();
                    Math.round(Math.sqrt(Math.pow(c, 2) + Math.pow(d, 2)));
                    a.fparent = a.opts.turn.data().fparent;
                    a.fparent || (c = g("<div/>", {
                        css: {
                            "pointer-events": "none"
                        }
                    }).hide(), c.data().flips = 0, c.css(m(0, 0, "auto", "visible").css).appendTo(a.opts.turn), a.opts.turn.data().fparent = c, a.fparent = c);
                    this.css({
                        position: "absolute",
                        top: 0,
                        left: 0,
                        bottom: "auto",
                        right: "auto"
                    });
                    a.wrapper =
                            g("<div/>", m(0, 0, this.css("z-index"))).appendTo(b).prepend(this);
                    a.fwrapper = g("<div/>", m(b.offset().top, b.offset().left)).hide().appendTo(a.fparent);
                    a.fpage = g("<div/>", {
                        css: {
                            cursor: "default"
                        }
                    }).appendTo(g("<div/>", m(0, 0, 0, "visible")).appendTo(a.fwrapper));
                    a.opts.gradients && (a.ashadow = g("<div/>", m(0, 0, 1)).appendTo(a.fpage));
                    i.setData.call(this, a)
            }
            i.resize.call(this, !0)
        },
        _fold: function (a) {
            var b = this.data().f, c = i._c.call(this, a.corner), d = this.width(), e = this.height();
            switch (b.effect) {
                case "hard":
                    a.x =
                        "l" == a.corner ? Math.min(Math.max(a.x, 0), 2 * d) : Math.max(Math.min(a.x, d), -d);
                    var f, g, h, H, r, k = b.opts.turn.data().totalPages, m = b.opts["z-index"] || k, o = {
                        overflow: "visible"
                    },
                        C = c.x ? (c.x - a.x) / d : a.x / d, n = 90 * C, s = 90 > n;
                    switch (a.corner) {
                        case "l":
                            H = "0% 50%";
                            r = "100% 50%";
                            s ? (f = 0, g = 0 < b.opts.next - 1, h = 1) : (f = "100%", g = b.opts.page + 1 < k, h = 0);
                            break;
                        case "r":
                            H = "100% 50%", r = "0% 50%", n = -n, d = -d, s ? (f = 0, g = b.opts.next + 1 < k, h = 0) : (f = "-100%", g = 1 != b.opts.page, h = 1)
                    }
                    o[x + "perspective-origin"] = r;
                    b.wrapper.transform("rotateY(" + n + "deg)translate3d(0px, 0px, " +
                        (this.attr("depth") || 0) + "px)", r);
                    b.fpage.transform("translateX(" + d + "px) rotateY(" + (180 + n) + "deg)", H);
                    b.parent.css(o);
                    s ? (C = -C + 1, b.wrapper.css({
                        zIndex: m + 1
                    }), b.fpage.css({
                        zIndex: m
                    })) : (C -= 1, b.wrapper.css({
                        zIndex: m
                    }), b.fpage.css({
                        zIndex: m + 1
                    }));
                    b.opts.gradients && (g ? b.ashadow.css({
                        display: "",
                        left: f,
                        backgroundColor: "rgba(0,0,0," + 0.5 * C + ")"
                    }).transform("rotateY(0deg)") : b.ashadow.hide(),
                        b.bshadow.css({
                            opacity: -C + 1
                        }), s ? b.bshadow.parent()[0] != b.wrapper[0] && b.bshadow.appendTo(b.wrapper) : b.bshadow.parent()[0] != b.fpage[0] &&
                        b.bshadow.appendTo(b.fpage), M(b.bshadow, j(100 * h, 0), j(100 * (-h + 1), 0), [[0, "rgba(0,0,0,0.3)"], [1, "rgba(0,0,0,0)"]], 2));
                    break;
                case "sheet":
                    var u = this, v = 0, A, D, E, K, y, L, w = j(0, 0), B = j(0, 0), l = j(0, 0);
                    f = i._foldingPage.call(this);
                    Math.tan(0);
                    var t = b.opts.acceleration, N = b.wrapper.height(), F = "t" == a.corner.substr(0, 1), z = "l" == a.corner.substr(1, 1), I = function () {
                        var f = j(c.x ? c.x - a.x : a.x, c.y ? c.y - a.y : a.y), g = j(z ? d - f.x / 2 : a.x + f.x / 2, f.y / 2), h = 2 > f.x && 2 > f.y ? G / 2 : G - Math.atan2(f.y, f.x), k = h - Math.atan2(g.y, g.x), k = Math.max(0, Math.sin(k) *
                            Math.sqrt(Math.pow(g.x, 2) + Math.pow(g.y, 2)));
                        v = 180 * (h / J);
                        l = j(k * Math.sin(h), k * Math.cos(h));
                        if (h > G && (l.x += Math.abs(l.y * f.y / f.x), l.y = 0, Math.round(l.x * Math.tan(J - h)) < e))
                            return a.y = Math.sqrt(Math.pow(e, 2) + 2 * g.x * f.x), F && (a.y = e - a.y), I();
                        h > G && (f = J - h, g = N - e / Math.sin(f), w = j(Math.round(g * Math.cos(f)), Math.round(g * Math.sin(f))), z && (w.x = -w.x), F && (w.y = -w.y));
                        A = Math.round(l.y / Math.tan(h) + l.x);
                        f = d - A;
                        g = f * Math.cos(2 * h);
                        k = f * Math.sin(2 * h);
                        B = j(Math.round(z ? f - g : A + g), Math.round(F ? k : e - k));
                        y = f * Math.sin(h);
                        f = i._c2.call(u, a.corner);
                        f = Math.sqrt(Math.pow(f.x - a.x, 2) + Math.pow(f.y - a.y, 2));
                        L = f < d ? f / d : 1;
                        b.opts.gradients && (K = 100 < y ? (y - 100) / y : 0, D = j(100 * (y * Math.sin(G - h) / e), 100 * (y * Math.cos(G - h) / d)), F && (D.y = 100 - D.y), z && (D.x = 100 - D.x));
                        i._backGradient.call(u) && (E = j(100 * (y * Math.sin(h) / d), 100 * (y * Math.cos(h) / e)), z || (E.x = 100 - E.x), F || (E.y = 100 - E.y));
                        l.x = Math.round(l.x);
                        l.y = Math.round(l.y);
                        return !0
                    };
                    g = function (a, c, f, g) {
                        var h = ["0", "auto"], k = (d - N) * f[0] / 100, l = (e - N) * f[1] / 100, c = {
                            left: h[c[0]],
                            top: h[c[1]],
                            right: h[c[2]],
                            bottom: h[c[3]]
                        },
                            h = 90 != g && -90 != g ? z ?
                            -1 : 1 : 0,
                            f = f[0] + "% " + f[1] + "%";
                        u.css(c).transform(q(g) + p(a.x + h, a.y, t), f);
                        b.fpage.parent().css(c);
                        b.wrapper.transform(p(-a.x + k - h, -a.y + l, t) + q(-g), f);
                        b.fwrapper.transform(p(-a.x + w.x + k, -a.y + w.y + l, t) + q(-g), f);
                        b.fpage.parent().transform(q(g) + p(a.x + B.x - w.x, a.y + B.y - w.y, t), f);
                        b.opts.gradients && M(b.ashadow, j(z ? 100 : 0, F ? 100 : 0), j(D.x, D.y), [[K, "rgba(0,0,0,0)"], [0.8 * (1 - K) + K, "rgba(0,0,0," + 0.2 * L + ")"], [1, "rgba(255,255,255," + 0.2 * L + ")"]], 3, 0);
                        i._backGradient.call(u) && M(b.bshadow, j(z ? 0 : 100, F ? 0 : 100), j(E.x, E.y), [[0.8, "rgba(0,0,0,0)"],
                            [1, "rgba(0,0,0," + 0.3 * L + ")"], [1, "rgba(0,0,0,0)"]], 3)
                    };
                    switch (a.corner) {
                        case "tl":
                            a.x = Math.max(a.x, 1);
                            I();
                            g(l, [1, 0, 0, 1], [100, 0], v);
                            b.fpage.transform(p(-e, -d, t) + q(90 - 2 * v), "100% 100%");
                            f.transform(q(90) + p(0, -e, t), "0% 0%");
                            break;
                        case "tr":
                            a.x = Math.min(a.x, d - 1);
                            I();
                            g(j(-l.x, l.y), [0, 0, 0, 1], [0, 0], -v);
                            b.fpage.transform(p(0, -d, t) + q(-90 + 2 * v), "0% 100%");
                            f.transform(q(270) + p(-d, 0, t), "0% 0%");
                            break;
                        case "bl":
                            a.x = Math.max(a.x, 1);
                            I();
                            g(j(l.x, -l.y), [1, 1, 0, 0], [100, 100], -v);
                            b.fpage.transform(p(-e, 0, t) + q(-90 + 2 * v), "100% 0%");
                            f.transform(q(270) + p(-d, 0, t), "0% 0%");
                            break;
                        case "br":
                            a.x = Math.min(a.x, d - 1), I(), g(j(-l.x, -l.y), [0, 1, 1, 0], [0, 100], v), b.fpage.transform(q(90 - 2 * v), "0% 0%"), f.transform(q(90) + p(0, -e, t), "0% 0%")
                    }
            }
            b.point = a
        },
        _moveFoldingPage: function (a) {
            var b = this.data().f;
            if (b) {
                var c = b.opts.turn, d = c.data(), e = d.pagePlace;
                a ? (d = b.opts.next, e[d] != b.opts.page && (i._foldingPage.call(this).appendTo(b.fpage), e[d] = b.opts.page, i.setData.call(this, {
                    folding: d
                })), c.turn("update")) : b.folding && (d.pages[b.folding] ? (c = d.pages[b.folding].data().f,
                    d.pageObjs[b.folding].appendTo(c.wrapper)) : d.pageWrap[b.folding] && d.pageObjs[b.folding].appendTo(d.pageWrap[b.folding]),
                    b.folding in e && (e[b.folding] = b.folding), delete b.folding)
            }
        },
        _showFoldedPage: function (a, b) {
            var c = i._foldingPage.call(this), d = this.data(), e = d.f;
            if (e) {
                if (!e.point || e.point.corner != a.corner) {
                    var f = g.Event("start");
                    this.trigger(f, [e.opts, a.corner]);
                    if (f.isDefaultPrevented())
                        return !1
                }
                if (c) {
                    if (b) {
                        var h = this, c = e.point && e.point.corner == a.corner ? e.point : i._c.call(this, a.corner, 1);
                        this.animatef({
                            from: [c.x,
                                c.y], to: [a.x, a.y],
                            duration: 500,
                            frame: function (b) {
                                a.x = Math.round(b[0]);
                                a.y = Math.round(b[1]);
                                i._fold.call(h, a)
                            }
                        })
                    }
                    else
                        i._fold.call(this, a), d.effect && !d.effect.turning && this.animatef(!1);
                    if (!e.visible)
                        switch (e.effect) {
                        case "hard":
                            e.visible = !0;
                            i._moveFoldingPage.call(this, !0);
                            e.fpage.show();
                            e.opts.shadows && e.bshadow.show();
                            break;
                        case "sheet":
                            e.visible = !0, e.fparent.show().data().flips++, i._moveFoldingPage.call(this, !0), e.fwrapper.show(), e.bshadow && e.bshadow.show()
                    }
                    return !0
                }
                return !1
            }
        },
        hide: function () {
            var a =
                this.data().f, b = i._foldingPage.call(this);
            switch (a.effect) {
                case "hard":
                    a.opts.gradients && (a.bshadowLoc = 0, a.bshadow.remove(), a.ashadow.hide());
                    a.wrapper.transform("");
                    a.fpage.hide();
                    break;
                case "sheet":
                    0 === --a.fparent.data().flips && a.fparent.hide(), this.css({
                        left: 0,
                        top: 0,
                        right: "auto",
                        bottom: "auto"
                    }).transform(""), a.wrapper.transform(""), a.fwrapper.hide(), a.bshadow && a.bshadow.hide(), b.transform("")
            }
            a.visible = !1;
            return this
        },
        hideFoldedPage: function (a) {
            var b = this.data().f;
            if (b.point) {
                var c = this, d = b.point,
                    e = function () {
                        b.point = null;
                        b.status = "";
                        c.flip("hide");
                        c.trigger("end", [b.opts, !1])
                    };
                if (a) {
                    var f = i._c.call(this, d.corner), a = "t" == d.corner.substr(0, 1) ? Math.min(0, d.y - f.y) / 2 : Math.max(0, d.y - f.y) / 2, g = j(d.x, d.y + a), h = j(f.x, f.y - a);
                    this.animatef({
                        from: 0,
                        to: 1,
                        frame: function (a) {
                            a = P(d, g, h, f, a);
                            d.x = a.x;
                            d.y = a.y;
                            i._fold.call(c, d)
                        },
                        complete: e,
                        duration: 800,
                        hiding: !0
                    })
                }
                else
                    this.animatef(!1), e()
            }
        },
        turnPage: function (a) {
            var b = this, c = this.data().f, a = {
                corner: c.corner ? c.corner.corner : a || i._cAllowed.call(this)[0]
            },
                d = c.point ||
                i._c.call(this, a.corner, c.opts.turn ? c.opts.turn.data().opts.elevation : 0), e = i._c2.call(this, a.corner);
            this.trigger("flip").animatef({
                from: 0,
                to: 1,
                frame: function (c) {
                    c = P(d, d, e, e, c);
                    a.x = c.x;
                    a.y = c.y;
                    i._showFoldedPage.call(b, a)
                },
                complete: function () {
                    b.trigger("end", [c.opts, !0])
                },
                duration: c.opts.duration,
                turning: !0
            });
            c.corner = null
        },
        moving: function () {
            return "effect" in this.data()
        },
        isTurning: function () {
            return this.flip("moving") && this.data().effect.turning
        },
        corner: function () {
            return this.data().f.corner
        },
        _eventStart: function (a) {
            var b =
                this.data().f, c = b.opts.turn;
            if (!b.disabled && !this.flip("isTurning") && b.opts.page == c.data().pagePlace[b.opts.page]) {
                b.corner = i._isIArea.call(this, a);
                if (b.corner)
                    return this.trigger("pressed", [b.point]), i._showFoldedPage.call(this, b.corner), !1;
                b.corner = null
            }
        },
        _eventMove: function (a) {
            var b = this.data().f;
            if (!b.disabled)
                if (a = u ? a.originalEvent.touches : [a], b.corner) {
                    var c = b.parent.offset();
                    b.corner.x = a[0].pageX - c.left;
                    b.corner.y = a[0].pageY - c.top;
                    i._showFoldedPage.call(this, b.corner)
                }
                else
                    b.hover && !this.data().effect &&
                    this.is(":visible") && ((a = i._isIArea.call(this, a[0])) ? (b.status = "hover", b = i._c.call(this, a.corner, b.opts.cornerSize / 2), a.x = b.x, a.y = b.y, i._showFoldedPage.call(this, a, !0)) : "hover" == b.status && (b.status = "", i.hideFoldedPage.call(this, !0)))
        },
        _eventEnd: function () {
            var a = this.data().f, b = a.corner;
            if (!a.disabled && b) {
                var c = g.Event("released");
                this.trigger(c, [a.point || b]);
                c.isDefaultPrevented() || i.hideFoldedPage.call(this, !0)
            }
            a.corner = null
        },
        disable: function (a) {
            i.setData.call(this, {
                disabled: a
            });
            return this
        },
        hover: function (a) {
            i.setData.call(this,
                {
                    hover: a
                });
            return this
        },
        peel: function (a, b) {
            var c = this.data().f;
            if (a) {
                if (-1 == g.inArray(a, i._cAllowed.call(this)))
                    throw n("Corner " + a + " is not permitted");
                var d = i._c.call(this, a, c.opts.cornerSize / 2);
                c.status = "peel";
                i._showFoldedPage.call(this, {
                    corner: a,
                    x: d.x,
                    y: d.y
                },
                    b)
            }
            else
                c.status = "", i.hideFoldedPage.call(this, b);
            return this
        }
    };
        window.requestAnim = function () {
            return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame ||
            function (a) {
                window.setTimeout(a, 1E3 / 60)
            }
        } ();
        g.extend(g.fn, {
            flip: function (a, b) {
                return O(g(this[0]), i, arguments)
            },
            turn: function (a) {
                return O(g(this[0]), h, arguments)
            },
            transform: function (a, b) {
                var c = {};
                b && (c[x + "transform-origin"] = b);
                c[x + "transform"] = a;
                return this.css(c)
            },
            animatef: function (a) {
                var b = this.data();
                b.effect && b.effect.stop();
                if (a) {
                    a.to.length || (a.to = [a.to]);
                    a.from.length || (a.from = [a.from]);
                    for (var c = [], d = a.to.length, e = !0, f = this, h = (new Date).getTime(), i = function () {
                        if (b.effect && e) {
                            for (var g = [],
                            j = Math.min(a.duration, (new Date).getTime() - h), m = 0;
                            m < d;
                            m++) g.push(b.effect.easing(1, j, a.from[m], c[m], a.duration));
                            a.frame(1 == d ? g[0] : g);
                            j == a.duration ? (delete b.effect, f.data(b), a.complete && a.complete()) : requestAnim(i)
                        }
                    },
                    j = 0;
                    j < d;
                    j++) c.push(a.to[j] - a.from[j]);
                    b.effect = g.extend({
                        stop: function () {
                            e = !1
                        },
                        easing: function (a, b, c, d, e) {
                            return d * Math.sqrt(1 - (b = b / e - 1) * b) + c
                        }
                    },
                    a);
                    this.data(b);
                    i()
                }
                else
                    delete b.effect
            }
        });
        g.isTouch = u;
        g.mouseEvents = s;
        g.cssPrefix = R;
        g.cssTransitionEnd = function () {
            var a, b = document.createElement("fakeelement"),
            c = {
                transition: "transitionend",
                OTransition: "oTransitionEnd",
                MSTransition: "transitionend",
                MozTransition: "transitionend",
                WebkitTransition: "webkitTransitionEnd"
            };
            for (a in c)
                if (void 0 !== b.style[a])
                    return c[a]
            };
            g.findPos = B
        })(jQuery);
    