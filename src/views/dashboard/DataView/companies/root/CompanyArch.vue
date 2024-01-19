<script>
import SharkTree from "@/components/SharkTree.vue";
import {Org} from "@/views/dashboard/DataView/companies/Org";
import {CompanyService} from "@/services/CompanyService";
import {PositionService} from "@/services/PositionService";
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import Life from "@/utils/classes/Life";
import PositionClass from "@/views/dashboard/DataView/companies/PositionClass";
import ReqiurementClass from "@/core/ModelClass";

export default{
components: {CollectionManager, SharkTree},
props:{
  positions:Array,
  emitter:undefined,
  companyId:Number,
  setToPosition:undefined
},
data(){
return{
  org:new Org(),
  service: new PositionService(this.emitter),
  companyService: new CompanyService(this.emitter),
  company:CompanyClass,
  life:new Life(),
  position:undefined,
}
},
methods:{
  onNodeSelect(node){
    console.log('on node select', node)
  },
  handleDragOver(){
    console.log('handledragover')
  },
  handleDrop(event, node){
    this.service.move(
        {
                    positionFrom:this.org.dragged,
                    positionTo:node,
    }).then(res =>
    {
      this.org.dragged = null;
      this.companyService.getById(this.companyId).then(res=>{
        if(res.status !== 200 || !res.data){
          return;
        }
        this.org.setTree(res.data.positions)
        if(this.$refs.sharkTree?.expandAll && this.org.expandAll){setTimeout(()=>{this.$refs.sharkTree.expandAll()}, 300)}
        this.company = CompanyClass.toModel(res.data, CompanyClass)
      })
    })

  },
  handleDragEnd(){
    console.log('handledragend')
  },
  handleDragStart(event, node){
    this.org.dragged = node;
    console.log('handledragstart', event,node)
  },
  handleDragLeave(){
    console.log('handledragleave')
  },
  handleDemandHover(event, hint){
    this.org.draw({top:event.target.getBoundingClientRect().top-27, left:event.target.getBoundingClientRect().left - 150},
        hint
      )
  },
  handleDemandHoverOut(){
    this.org.rubber()
  },
  handleDemandClick(node, demand){
    switch(demand){
      case "employees":
        this.position = PositionClass.toModel(node, PositionClass);
        this.life.collectionKey='employees';
        this.life.collection = this.position.employees
        this.life.modules = ['select', 'delete']
            break;
      case "requirements":
        this.position = PositionClass.toModel(node, PositionClass);
        this.life.collectionKey='requirements';
        this.life.collection=this.position.requirements
        this.life.modules = ['create','delete']
            break;
    }
  },
  setCollection(collection, key){
    this.life.collectionKey = key
    if(collection) {
      this.position[key] = collection
      const positionInList = this.positions.find(p=>p.id === this.position.id)
      if(positionInList){
        positionInList[key]  = collection;
      }
      console.log('pazdan',collection, key, this.position)
     this.setToPosition(collection, key, this.position)

    }
  },

},
mounted(){
  this.companyService.getById(this.companyId).then(res=>{
    if(res.status === 200 && res.data){
      this.company = CompanyClass.toModel(res.data, CompanyClass)
    }
  })
  this.org.setTree(this.positions)
  this.$nextTick(()=>{
    if(this.$refs.sharkTree?.expandAll){
      this.$refs.sharkTree.expandAll()
    }
  })
},
watch:{
  "org.expandAll"(){
    if(this.org.expandAll){
      this.$refs.sharkTree.expandAll()
    }else{
      this.$refs.sharkTree.hideAll()
    }
  }
}
}
</script>

<template>
  <div v-if="life.collectionKey">
    <CollectionManager :modules="this.life.modules??[]"
                       :service="service"
                       :owner="position"
                       owner-key="position"
                       :emitter="emitter"
                       :set-collection="setCollection"
                       :collection-key="this.life.collectionKey"
                       :collection="life.collection"></CollectionManager>
  </div>
  <div style="font-size:0.8em; width:100%; border-bottom:solid 1px #00000020;margin-bottom:8px;color:#00000090">
  <input type="checkbox" v-model="org.expandAll" />
    Expand
  </div>
  <shark-tree ref="sharkTree"
              :key="[org.tree, company]"
              :value="org.tree"
              :handle-drag-end="handleDragEnd"
              :handle-drag-leave="handleDragLeave"
              :handle-drag-over="handleDragOver"
              :handle-demand-hover="handleDemandHover"
              :handle-demand-hover-out="handleDemandHoverOut"
              :handle-drag-start="handleDragStart"
              :handle-drop="handleDrop"
              :company = "company"
              :handle-demand-click="handleDemandClick"
              :node-select="onNodeSelect"></shark-tree>

</template>

<style>

</style>