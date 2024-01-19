<script >
import {CrudBunch, ServiceBunch} from "@/core/Bunch";
import { inject } from "vue";
export default{
  props:{
    params:undefined,
  },
  setup(){
    const emitter = inject('emitter');
    return {
      emitter
    }
  },
  data(){
    return {
      model:undefined,
      crud: new CrudBunch[this.params.colDef.owner](new ServiceBunch[this.params.colDef.owner](this.emitter)),
      service:new ServiceBunch[this.params.colDef.field](),
      data:[],
      selection:null,
    }
  },
  async mounted(){
  this.model = await this.crud.getById(this.params.data.id)

    this.service.getAll().then(res=>{
      if(res.status === 200){
        this.data = res.data
        if(this.params.value){
          const sel = this.data.find(d=>d.id === this.params.value.id)
          if(sel) this.selection = sel
        }
      }
    })
  },
  methods:{
    filterInputCollection(inputCollection, param){
      if(this.model?.$filters[param] ){
        return this.model?.$filters[param](this.model, inputCollection)
      }
      return inputCollection;
    },
    select(){
      this.params.setValue(this.selection)
    }
  },
  watch:{

  }
}
</script>

<template>
<div :key="selection" class="flex-center-center">
  <select  class="select-renderer" v-model="selection" :key="model" @change="select">
    <option :value="null"></option>
    <option v-for="option in filterInputCollection(this.data, this.params.colDef.field)" :key="option" :value="option">
      {{option.code ? `${option.code} ${option?.description ?? ''}` : (option.firstName ? `${option.firstName} ${option.lastName}`:``)}}
    </option>
  </select>
</div>
</template>

<style>

</style>