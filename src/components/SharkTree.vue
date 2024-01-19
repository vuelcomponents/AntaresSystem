<script>
export default {
  name: "SharkTree",
  props:{
    value:undefined,
    chooser:undefined,
    position:undefined,
    handleDemandClick:{
      type:Function,
      default:()=>{}
    },
    nodeSelect:{
      type:Function,
      default:()=>{}
    },
    handleDragStart:{
      type:Function,
      default:()=>{}
    },
    handleDragOver:{
      type:Function,
      default:()=>{}
    },
    handleDrop:{
      type:Function,
      default:()=>{}
    },
    handleDragLeave:{
      type:Function,
      default:()=>{}
    },
    handleDragEnd:{
      type:Function,
      default:()=>{}
    },
    handleDemandHover:{
      type:Function,
      default:()=>{}
    },
    handleDemandHoverOut:{
      type:Function,
      default:()=>{}
    },
  },
  components:{},
  mounted(){
    this.depth();
    this.iconize();
  },
  data(){
    return{
      expanded:[],
      chosen:null,
      tree:this.value,
    }
  },
  methods:{
    handleExpand(node, e){
      if(e.target.getAttribute('tag') === 'btt'){
        console.log('btt')
        return;
      }
      node.expanded === null ? node.expanded = true : node.expanded = !node.expanded;
    },
    select(node){
      if(this.chooser && node?.unitType === 3){
        this.chosen = node.id
        console.log('chosen/', this.chosen)
      }
      this.nodeSelect(node)
    },
    depth(tree = this.tree, currentDepth = 0){
      for (const item of tree) {
        item.depth = currentDepth;

        if (item.children && item.children.length > 0) {
          this.depth(item.children, currentDepth + 1);
        }
      }
    },
    expandAll(tree = this.tree){
      for (const item of tree) {
        item.expanded = true;
        if (item.children && item.children.length > 0) {
          this.expandAll(item.children);
        }
      }
    },
    hideAll(tree = this.tree){
      for (const item of tree) {
        item.expanded = false;
        if (item.children && item.children.length > 0) {
          this.hideAll(item.children);
        }
      }
    },
    iconize(tree = this.tree){
      for (const item of tree) {
        switch(item?.positionUnit?.code){
          case null:
            item.icon = 'mdi mdi-lock-off'
            break;
          case 'Business Unit':
            item.icon = 'mdi mdi-chart-line-variant'
            break;
          case 'Position':
            item.icon='mdi mdi-account-hard-hat-outline'
            break;
        }

        if (item.children && item.children.length > 0) {
          this.iconize(item.children);
        }
      }
    },
    handleEmployeeClick(e){
      e.preventDefault()
      console.log(e)
    },
    handleRequirementClick(e){

    }
  }

}
</script>

<template>
  <div v-for="node in tree" :key="node.id">
    <div :draggable="true"
         @click="(event)=>{handleExpand(node, event); select(node)}"
         :class="['node-h', this.chosen === node.id || this.position?.id === node.id ? 'chosen-green': '']"
         @dragstart="handleDragStart($event, node)"
         @dragover.prevent="handleDragOver($event, node)"
         @drop.prevent="handleDrop($event, node)"
         @dragleave="handleDragLeave($event, node)"
         @dragend="handleDragEnd($event, node)"
         :style="{
           color: chooser ? ( node.unitType === 3 ? '#1d2c88' : (node?.children?.length> 0 ? '#00000090' : '#00000040') ): '#00000099',
           marginLeft: node?.depth*15 + 'px',
           fontWeight: chooser ? ( node.unitType === 3 ? 'bolder'  : 'initial') : 'initial',
           display:'grid', gridTemplateColumns:'1fr 1fr'

         }"
         :id="node.id"
         tag="tt-node"
    ><div>
        <span style="margin-right:10px;">
          <i :class="node.icon"></i>
        </span>
        <span>
         {{ node.code }}
        </span>
    </div>
      <div :style="{textAlign:'right', display:'inline'}">
        <div class="btt"  style="min-width:300px;display:inline;text-align:left" v-if="node.positionUnit?.code === 'Position'">
          <span style="min-width:300px;">
           <i tag="btt" @click="handleDemandClick(node, 'requirements')" @mouseout="handleDemandHoverOut($event )" @mouseenter="handleDemandHover($event, 'Configure requirements')" class="mdi mdi-asterisk-circle-outline"></i>
          </span>
        </div>
        <div class="btt" style="min-width:300px;display:inline;" v-if="node.positionUnit?.code === 'Position'">
          <i tag="btt"   @click="handleDemandClick(node, 'employees')" @mouseout="handleDemandHoverOut($event )" @mouseenter="handleDemandHover($event, 'Employees in position')" class="mdi mdi-account-multiple" style="margin-left:15px"></i>
        </div>
        <div class="btt" style="display:inline;">
          <span tag="btt" @click="handleDemandClick" @mouseout="handleDemandHoverOut($event)" @mouseenter="handleDemandHover($event, 'Resources / Demand')" style="margin-left:15px; min-width:250px; text-align:left">
            {{node.employeesCount ?? node.employees.length}} / {{node.demand ?? '∞'}}
          </span>
        </div>
      </div>
    </div>
    <span :hidden="!node.expanded">
    <SharkTree v-if="node.children"
                  :value="node.children"
                  :node-select="nodeSelect"
                  :handle-drag-over="handleDragOver"
                  :handle-drag-start="handleDragStart"
                  :handle-drop="handleDrop"
                  :handle-drag-leave="handleDragLeave"
                  :handle-drag-end="handleDragEnd"
                  :handle-demand-hover="handleDemandHover"
                  :handle-demand-hover-out="handleDemandHoverOut"
                  :chooser="chooser"
                  :position="position"
                  :handle-demand-click="handleDemandClick"
                 ></SharkTree>
    </span>
  </div>
</template>

<style >
.tt-tree{
  text-align:left;
  padding-inline:10px;
}
.node-h{
  transition:all 0.2s ease;
  border-radius: 3px;
  padding:2px;
}
.node-h:hover{
  background: #adbcd230;

  cursor:pointer;
}
.chosen-green{
  color:red!important;
}
.btt:hover{
  i{
    color: #0090ff;
  }
}
</style>