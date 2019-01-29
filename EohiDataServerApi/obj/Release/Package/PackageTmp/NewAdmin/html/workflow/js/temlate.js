
<script id="tpl-audio" type="text/html">
  <div class='pa' id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <a class='btn btn-default' href='#' role='button'> 放音
      <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
    </a>
  </div>
</script>

<script id="tpl-demo" type="text/html">
  <div class='pa' id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <a class='btn btn-default' href='#' role='button'> {{name}}
      <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
    </a>
  </div>
</script>

<script id="tpl-Announce" type="text/html">
  <div class='pa' id='{{id}}' style='top:{{top}}px;left:{{left}}px'>

    <a class='btn btn-default' href='#' role='button'>
      <i class="fa fa-play-circle-o" aria-hidden="true"></i>
{{name}}
<span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
</a>
</div>
</script>

<script id="tpl-menu" type="text/html">
  <div class="pa" id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <div class="panel panel-default panel-node panel-info">
      <div id='{{id}}-heading' data-id="{{id}}" class="panel-heading">菜单
        <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
      </div>
      <ul class="list-group">
        <li id="{{generateId}}" data-pid="{{id}}" class="list-group-item panel-node-list">按1
        </li>
        <li id="{{generateId}}" data-pid="{{id}}" class="list-group-item panel-node-list">按2
        </li>
        <li id="{{generateId}}" data-pid="{{id}}" class="list-group-item panel-node-list">按3
        </li>
      </ul>
    </div>
  </div>
</script>

<script id="tpl-Root" type="text/html">
  <div class='pa' id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <a class='btn btn-success' href='#' role='button'> {{name}}
      <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
    </a>
  </div>
</script>

<script id="tpl-Exit" type="text/html">
  <div class='pa' id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <a class='btn btn-danger' href='#' role='button'> {{name}}
      <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
    </a>
  </div>
</script>

<script id="tpl-WorkTime" type="text/html">
  <div class="pa" id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <div class="panel panel-default panel-node panel-info">
      <div id='{{id}}-heading' data-id="{{id}}" class="panel-heading"><i class="fa fa-calendar-times-o" aria-hidden="true"></i> {{name}}
        <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
      </div>
      <ul class="list-group">
        <li id="{{id}}-onWorkTime" data-pid="{{id}}" class="list-group-item panel-node-list">工作时间
        </li>
        <li id="{{id}}-offWorkTime" data-pid="{{id}}" class="list-group-item panel-node-list">非工作时间
        </li>
      </ul>
    </div>
  </div>
</script>

<script id="tpl-Menu" type="text/html">
  <div class="pa" id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
    <div class="panel panel-default panel-node panel-info">
      <div id='{{id}}-heading' data-id="{{id}}" class="panel-heading"><i class="fa fa-navicon" aria-hidden="true"></i> {{name}}
        <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
      </div>
      <ul class="list-group">
        <li id="{{id}}-noinput" data-pid="{{id}}" class="list-group-item panel-node-list">输入超时
        </li>
        <li id="{{id}}-nomatch" data-pid="{{id}}" class="list-group-item panel-node-list">输入错误
        </li>
{{#choices}}
<li id="{{id}}-key-{{key}}" data-pid="{{id}}" class="list-group-item panel-node-list">按{{key}}
</li>{{/choices}}
        </ul>
      </div>
    </div>
  </script>

  <script id="tpl-Route" type="text/html">
    <div class="pa" id='{{id}}' style='top:{{top}}px;left:{{left}}px'>
      <div class="panel panel-default panel-node panel-info">
        <div id='{{id}}-heading' data-id="{{id}}" class="panel-heading"><i class="fa fa-navicon" aria-hidden="true"></i> {{name}}
          <span class="delete-node pull-right" data-type="deleteNode" data-id="{{id}}">X</span>
        </div>
        <ul class="list-group">
          <li id="{{id}}-noinput" data-pid="{{id}}" class="list-group-item panel-node-list">输入超时
          </li>
          <li id="{{id}}-nomatch" data-pid="{{id}}" class="list-group-item panel-node-list">输入错误
          </li>
{{#choices}}
<li id="{{id}}-key-{{key}}" data-pid="{{id}}" class="list-group-item panel-node-list">按{{key}}
</li>{{/choices}}
        </ul>
      </div>
    </div>
  </script>