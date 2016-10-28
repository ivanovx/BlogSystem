/*!
 * Mvc.Grid 3.2.0
 * https://github.com/NonFactors/MVC5.Grid
 *
 * Copyright © NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */
var MvcGrid = (function () {
    function MvcGrid(grid, options) {
        this.columns = [];
        this.element = grid;
        options = options || {};
        this.name = grid.attr('id') || '';
        this.rowClicked = options.rowClicked;
        this.reloadEnded = options.reloadEnded;
        this.reloadFailed = options.reloadFailed;
        this.reloadStarted = options.reloadStarted;
        this.sourceUrl = options.sourceUrl || grid.data('source-url') || '';
        this.filters = $.extend({
            'Text': new MvcGridTextFilter(),
            'Date': new MvcGridDateFilter(),
            'Number': new MvcGridNumberFilter(),
            'Boolean': new MvcGridBooleanFilter()
        }, options.filters);

        if (this.sourceUrl) {
            var splitIndex = this.sourceUrl.indexOf('?');
            if (splitIndex > -1) {
                this.query = this.sourceUrl.substring(splitIndex + 1);
                this.sourceUrl = this.sourceUrl.substring(0, splitIndex);
            } else {
                this.query = options.query || '';
            }
        } else {
            this.query = window.location.search.replace('?', '');
        }

        if (options.reload || (this.sourceUrl && !options.isLoaded)) {
            this.reload(this);
            return;
        }

        var headers = grid.find('th');
        for (var i = 0; i < headers.length; i++) {
            var column = this.createColumn(this, $(headers[i]));
            this.bindFilter(this, column);
            this.bindSort(this, column);
            this.cleanup(this, column);
            this.columns.push(column);
        }

        var pager = grid.find('.mvc-grid-pager');
        if (pager.length > 0) {
            this.pager = {
                currentPage: pager.find('li.active').data('page') || 0,
                rowsPerPage: pager.find('.mvc-grid-pager-rows'),
                pages: pager.find('li:not(.disabled)'),
                element: pager
            };
        }

        this.bindPager(this);
        this.bindGrid(this);
        this.clean(this);
    }

    MvcGrid.prototype = {
        createColumn: function (grid, header) {
            var column = {};
            column.header = header;
            column.name = header.data('name') || '';

            if (header.data('filter') == 'True') {
                column.filter = {
                    isMulti: header.data('filter-multi') == 'True',
                    operator: header.data('filter-operator') || '',
                    name: header.data('filter-name') || '',
                    first: {
                        type: header.data('filter-first-type') || '',
                        val: header.data('filter-first-val') || ''
                    },
                    second: {
                        type: header.data('filter-second-type') || '',
                        val: header.data('filter-second-val') || ''
                    }
                };
            }

            if (header.data('sort') == 'True') {
                column.sort = {
                    firstOrder: header.data('sort-first') || '',
                    order: header.data('sort-order') || ''
                }
            }

            return column;
        },
        set: function (grid, options) {
            grid.filters = $.extend(grid.filters, options.filters);
            grid.rowClicked = options.rowClicked || grid.rowClicked;
            grid.reloadEnded = options.reloadEnded || grid.reloadEnded;
            grid.reloadFailed = options.reloadFailed || grid.reloadFailed;
            grid.reloadStarted = options.reloadStarted || grid.reloadStarted;

            if (options.reload) {
                grid.reload(grid);
            }
        },

        bindFilter: function (grid, column) {
            if (column.filter) {
                column.header.find('.mvc-grid-filter').on('click.mvcgrid', function () {
                    grid.renderFilter(grid, column);
                });
            }
        },
        bindSort: function (grid, column) {
            if (column.sort) {
                column.header.on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-filter').length == 0) {
                        grid.applySort(grid, column);
                        grid.reload(grid);
                    }
                });
            }
        },
        bindPager: function (grid) {
            if (grid.pager) {
                grid.pager.rowsPerPage.on('change', function () {
                    grid.applyPage(grid, grid.pager.currentPage);
                    grid.reload(grid);
                });

                grid.pager.pages.on('click.mvcgrid', function () {
                    var page = $(this).data('page');

                    if (page) {
                        grid.applyPage(grid, page);
                        grid.reload(grid);
                    }
                });
            }
        },
        bindGrid: function (grid) {
            grid.element.find('tbody tr').on('click.mvcgrid', function (e) {
                if (grid.rowClicked) {
                    var cells = $(this).find('td');
                    var data = [];

                    for (var ind = 0; ind < grid.columns.length; ind++) {
                        var column = grid.columns[ind];
                        if (cells.length > ind) {
                            data[column.name] = $(cells[ind]).text();
                        }
                    }

                    grid.rowClicked(grid, this, data, e);
                }
            });
        },

        reload: function (grid) {
            if (grid.reloadStarted) {
                grid.reloadStarted(grid);
            }

            if (grid.sourceUrl) {
                $.ajax({
                    cache: false,
                    url: grid.sourceUrl + '?' + grid.query
                }).done(function (result) {
                    grid.element.hide();
                    grid.element.after(result);

                    var newGrid = grid.element.next('.mvc-grid').mvcgrid({
                        reloadStarted: grid.reloadStarted,
                        reloadFailed: grid.reloadFailed,
                        reloadEnded: grid.reloadEnded,
                        rowClicked: grid.rowClicked,
                        sourceUrl: grid.sourceUrl,
                        filters: grid.filters,
                        query: grid.query,
                        isLoaded: true
                    }).data('mvc-grid');
                    grid.element.remove();

                    if (grid.reloadEnded) {
                        grid.reloadEnded(newGrid);
                    }
                })
                .fail(function (result) {
                    if (grid.reloadFailed) {
                        grid.reloadFailed(grid, result);
                    }
                });
            } else {
                window.location.href = '?' + grid.query;
            }
        },
        renderFilter: function (grid, column) {
            var popup = $('body').children('.mvc-grid-popup');
            var gridFilter = grid.filters[column.filter.name];

            if (gridFilter) {
                gridFilter.render(grid, popup, column.filter);
                gridFilter.init(grid, column, popup);

                grid.setFilterPosition(grid, column, popup);
                popup.addClass('open');

                $(window).on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-popup').length == 0 &&
                        !target.is('[class*="ui-datepicker"]') && target.parents('[class*="ui-datepicker"]').length == 0) {
                        $(window).off('click.mvcgrid');
                        popup.removeClass('open');
                    }
                });
            } else {
                $(window).off('click.mvcgrid');
                popup.removeClass('open');
            }
        },
        setFilterPosition: function (grid, column, popup) {
            var filter = column.header.find('.mvc-grid-filter');
            var arrow = popup.find('.popup-arrow');
            var filterLeft = filter.offset().left;
            var filterTop = filter.offset().top;
            var filterHeight = filter.height();
            var winWidth = $(window).width();
            var popupWidth = popup.width();

            var popupTop = filterTop + filterHeight / 2 + 14;
            var popupLeft = filterLeft - 8;
            var arrowLeft = 15;

            if (filterLeft + popupWidth + 5 > winWidth) {
                popupLeft = winWidth - popupWidth - 14;
                arrowLeft = filterLeft - popupLeft + 7;
            }

            arrow.css('left', arrowLeft + 'px');
            popup.css('left', popupLeft + 'px');
            popup.css('top', popupTop + 'px');
        },

        cancelFilter: function (grid, column) {
            grid.queryRemove(grid, grid.name + '-Page');
            grid.queryRemove(grid, grid.name + '-Rows');
            grid.queryRemoveStartingWith(grid, grid.name + '-' + column.name + '-');
        },
        applyFilter: function (grid, column) {
            grid.cancelFilter(grid, column);

            grid.queryAdd(grid, grid.name + '-' + column.name + '-' + column.filter.first.type, column.filter.first.val);
            if (column.filter.isMulti) {
                grid.queryAdd(grid, grid.name + '-' + column.name + '-Op', column.filter.operator);
                grid.queryAdd(grid, grid.name + '-' + column.name + '-' + column.filter.second.type, column.filter.second.val);
            }

            if (grid.pager) {
                grid.queryAdd(grid, grid.name + '-Rows', grid.pager.rowsPerPage.val());
            }
        },
        applySort: function (grid, column) {
            grid.queryRemove(grid, grid.name + '-Sort');
            grid.queryRemove(grid, grid.name + '-Order');
            grid.queryAdd(grid, grid.name + '-Sort', column.name);
            var order = column.sort.order == 'Asc' ? 'Desc' : 'Asc';
            if (!column.sort.order && column.sort.firstOrder) {
                order = column.sort.firstOrder;
            }

            grid.queryAdd(grid, grid.name + '-Order', order);
        },
        applyPage: function (grid, page) {
            grid.queryRemove(grid, grid.name + '-Page');
            grid.queryRemove(grid, grid.name + '-Rows');

            grid.queryAdd(grid, grid.name + '-Page', page);
            grid.queryAdd(grid, grid.name + '-Rows', grid.pager.rowsPerPage.val());
        },

        queryAdd: function (grid, key, value) {
            grid.query += (grid.query ? '&' : '') + encodeURIComponent(key) + '=' + encodeURIComponent(value);
        },
        queryRemoveStartingWith: function (grid, key) {
            var keyToRemove = encodeURIComponent(key);
            var params = grid.query.split('&');
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var paramKey = params[i].split('=')[0];
                if (params[i] && paramKey.indexOf(keyToRemove) != 0) {
                    newParams.push(params[i]);
                }
            }

            grid.query = newParams.join('&');
        },
        queryRemove: function (grid, key) {
            var keyToRemove = encodeURIComponent(key);
            var params = grid.query.split('&');
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var paramKey = params[i].split('=')[0];
                if (params[i] && paramKey != keyToRemove) {
                    newParams.push(params[i]);
                }
            }

            grid.query = newParams.join('&');
        },

        cleanup: function (grid, column) {
            var header = column.header;
            header.removeAttr('data-name');

            header.removeAttr('data-filter');
            header.removeAttr('data-filter-name');
            header.removeAttr('data-filter-multi');
            header.removeAttr('data-filter-operator');
            header.removeAttr('data-filter-first-val');
            header.removeAttr('data-filter-first-type');
            header.removeAttr('data-filter-second-val');
            header.removeAttr('data-filter-second-type');

            header.removeAttr('data-sort');
            header.removeAttr('data-sort-order');
            header.removeAttr('data-sort-first');
        },
        clean: function (grid) {
            grid.element.removeAttr('data-source-url');
        }
    };

    return MvcGrid;
})();

var MvcGridTextFilter = (function () {
    function MvcGridTextFilter() {
    }

    MvcGridTextFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Text;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.first.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="StartsWith"' + (filter.first.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.first.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                     '</div>' +
                     (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.second.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="StartsWith"' + (filter.second.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.second.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                     '</div>' :
                     '') +
                     '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                     '</div>' +
                 '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-input');
            inputs.on('keyup.mvcgrid', function (e) {
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(grid, column);
                grid.reload(grid);
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(grid, column);
                    grid.reload(grid);
                }
            });
        }
    };

    return MvcGridTextFilter;
})();

var MvcGridNumberFilter = (function () {
    function MvcGridNumberFilter() {
    }

    MvcGridNumberFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Number;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var filter = this;

            var inputs = popup.find('.mvc-grid-input');
            inputs.on('keyup.mvcgrid', function (e) {
                if (filter.isValid(this.value)) {
                    $(this).removeClass('invalid');
                    if (e.which == 13) {
                        popup.find('.mvc-grid-apply').click();
                    }
                } else {
                    $(this).addClass('invalid');
                }
            });

            for (var i = 0; i < inputs.length; i++) {
                if (!filter.isValid(inputs[i].value)) {
                    $(inputs[i]).addClass('invalid');
                }
            }
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(grid, column);
                grid.reload(grid);
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(grid, column);
                    grid.reload(grid);
                }
            });
        },

        isValid: function (value) {
            if (!value) return true;
            var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[.,]?\\d*$');

            return pattern.test(value);
        }
    };

    return MvcGridNumberFilter;
})();

var MvcGridDateFilter = (function () {
    function MvcGridDateFilter() {
    }

    MvcGridDateFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterInput = '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">';
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Date;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        filterInput +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        filterInput +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-input');
            if ($.fn.datepicker) { inputs.datepicker(); }

            inputs.on('change.mvcgrid keyup.mvcgrid', function (e) {
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(grid, column);
                grid.reload(grid);
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(grid, column);
                    grid.reload(grid);
                }
            });
        }
    };

    return MvcGridDateFilter;
})();

var MvcGridBooleanFilter = (function () {
    function MvcGridBooleanFilter() {
    }

    MvcGridBooleanFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Boolean;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.first.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.first.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.second.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.second.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-boolean-filter li');
            inputs.on('click.mvcgrid', function () {
                $(this).addClass('active').siblings().removeClass('active');
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.first.type = 'Equals';
                column.filter.second.type = 'Equals';
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.val = popup.find('.first-filter li.active').data('value');
                column.filter.second.val = popup.find('.second-filter li.active').data('value');

                grid.applyFilter(grid, column);
                grid.reload(grid);
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(grid, column);
                    grid.reload(grid);
                }
            });
        }
    };

    return MvcGridBooleanFilter;
})();

$.fn.mvcgrid = function (options) {
    return this.each(function () {
        if (!$.data(this, 'mvc-grid')) {
            $.data(this, 'mvc-grid', new MvcGrid($(this), options));
        } else if (options) {
            $.data(this, 'mvc-grid').set($.data(this, 'mvc-grid'), options);
        }
    });
};
$.fn.mvcgrid.lang = {
    Text: {
        Contains: 'Contains',
        Equals: 'Equals',
        NotEquals: 'Not equals',
        StartsWith: 'Starts with',
        EndsWith: 'Ends with'
    },
    Number: {
        Equals: 'Equals',
        NotEquals: 'Not equals',
        LessThan: 'Less than',
        GreaterThan: 'Greater than',
        LessThanOrEqual: 'Less than or equal',
        GreaterThanOrEqual: 'Greater than or equal'
    },
    Date: {
        Equals: 'Equals',
        NotEquals: 'Not equals',
        LessThan: 'Is before',
        GreaterThan: 'Is after',
        LessThanOrEqual: 'Is before or equal',
        GreaterThanOrEqual: 'Is after or equal'
    },
    Boolean: {
        Yes: 'Yes',
        No: 'No'
    },
    Filter: {
        Apply: '&#10004;',
        Remove: '&#10008;'
    },
    Operator: {
        Select: '',
        And: 'and',
        Or: 'or'
    }
};
$(function () {
    $('body').append('<div class="mvc-grid-popup"></div>');
    $(window).resize(function () {
        $('.mvc-grid-popup').removeClass('open');
    });
});
