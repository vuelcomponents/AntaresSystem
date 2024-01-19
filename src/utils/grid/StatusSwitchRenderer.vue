<script >
import {StatusService} from "@/services/StatusService";

export default{
  props:{
    params:undefined
  },
  data(){
    return {
      service:undefined,
      list:undefined,
      select:null,
      transitionTo:[]
    }
  },
  async mounted(){
    this.service =new StatusService()
    this.list = (await this.service.getAll())?.data
    if(this.list){
      this.list = this.list.filter(s=>s.storeModel.code === this.params?.colDef?.storeModel && !s.reserved)
    }

  },
  methods:{
    async loadList(){
      if(!this.service){
        this.service =new StatusService()
      }
      if(!this.list){
        this.list = (await this.service.getAll())?.data
        if(this.list) {
          this.list = this.list.filter(s => s.storeModel.code === this.params?.colDef?.storeModel && !s.reserved)
        }
      }
      if(this.params.data.status){
        this.transitionTo = this.params.data.status.transitionTo
      }
    },
    change(){

      if(this.select === 'clear'){
        this.params.setValue(null)
        this.select = null;
        return;
      }
      this.params.setValue(this.select)
    }

  }
}
</script>

<template>
<div class="status-box" >
  <div :hidden="!this.params?.data?.status">
    <i  class="mdi mdi-access-point" :style="`color:#${params.data?.status?.color}`"/>
    {{params.data?.status?.code}}
  </div>
  <div :hidden="this.params?.data?.status"  class="flex-center-center" style="width:100%;cursor:pointer;">
    <select class="select-renderer" v-model="select" @click="loadList" @change="change">
      <option :value="null" disabled>&nbsp;none</option>
      <option v-for="status in this.list" :value="status" :key="status">{{status?.code}}</option>
    </select>
  </div>
  <div :hidden="!this.params?.data?.status"  class="flex-center-center" style="width:100%;cursor:pointer;">
    <select class="select-renderer" v-model="select" @click="loadList" @change="change">
      <option :value="null" ></option>
      <option :value="`clear`" >--clear</option>
      <option v-for="status in this.transitionTo" :value="status" :key="status">{{status?.code}}</option>
    </select>
  </div>
</div>
</template>

<style>
.status-box{
  cursor:pointer!important;
  display:flex;
  align-items:center;
  gap:10px;
}

</style>