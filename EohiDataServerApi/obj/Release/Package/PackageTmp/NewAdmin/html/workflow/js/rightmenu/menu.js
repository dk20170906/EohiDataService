exampleMenuItemSource = function (selector) {
    if ($(selector).attr('id') == 'PNG_JPG') {
        return [
                {
                    header: 'Example Dynamic'
                },
                {
                    text: 'PNG',
                    action: function(e, selector) { alert('PNG clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
                },
                {
                    text: 'JPG',
                    action: function(e, selector) { alert('JPG clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
                },
                {   divider: true   },
                {
                    icon: 'glyphicon-list-alt',
                    text: 'Dynamic nested',
                    subMenu : [
                    {
                        text: 'More dynamic',
                        action: function(e, selector) { alert('More dynamic clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
                    },
                    {
                        text: 'And more...',
                        action: function(e, selector) { alert('And more... clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
                    }
                    ]
                }
            ]
    } else {
        return [
                {
                    icon: 'glyphicon-exclamation-sign',
                    text: 'No image types supported!'
                }
            ]
    }
}

test_menu = {
    id: 'TEST-MENU',
    data: [
        {
            header: '处理'
        },
        {
            icon: 'glyphicon-copy',
            text: '复制',
            action: function(e, selector) { alert('Create clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
        },
        {
            icon: 'glyphicon-edit',
            text: '编辑',
            action: function (e, selector) {

                openEditDialog(selector.attr("id"));
                //alert('Edit clicked on ' + selector.prop("tagName") + ":" + selector.attr("id"));
            }
        },
        {
            icon: 'glyphicon-list-alt',
            text: '端点显示',
            subMenu : [
            {
                text: '上下联接点',
                action: function(e, selector) { alert('Text clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
            },
            {
                text: '左右联接点',
                action: function(e, selector) { alert('Text clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
            }
            ]
        },
        {
            divider: true
        },
        {
            header: '其他操作'
        },
        {
            icon: 'glyphicon-trash',
            text: '删除',
            action: function (e, selector) {

                window.IVR.DeleteNode(selector.attr("id"));
                //alert('Delete clicked on ' + selector.prop("tagName") + ":" + selector.attr("id"));
               
            }
        }
    ]
};

test_menu2 = [
    {
        header: 'Example'
    },
    {
        icon: 'glyphicon-plus',
        text: 'Create',
        action: function(e, selector) { alert('Create clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
    },
    {
        icon: 'glyphicon-edit',
        text: 'Edit',
        action: function(e, selector) { alert('Edit clicked on ' + selector.prop("tagName") + ":" + selector.attr("id")); }
    }
];
