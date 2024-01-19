<script>
import {ServiceBunch} from "@/core/Bunch";

export default{
  props:{
    param:undefined,
    model:undefined,
    owner:undefined,
  },
  data(){
    return{
      service:undefined,
      data:[],
      selection:null,
      skey:0,
    }
  },
  mounted(){
    this.service = new ServiceBunch[this.param]()
    this.service.getAll().then(res=>{
      if(res.status === 200){
        this.data = res.data
        if(this.model[this.param]){
          console.log('xman', this.model[this.param], this.data.find(d=>d.id === this.model[this.param]?.id))
          this.selection = this.data.find(d=>d.id === this.model[this.param]?.id)  ?? null
        }
      }
    })
  },
  methods:{
    filterInputCollection(inputCollection, param){
      if(this.model.$filters[param] ){
        return this.model.$filters[param](this.model, inputCollection, this.owner)
      }
      return inputCollection;
    },
    makeSelection(){
      this.model.setParam(this.param, this.selection)
    },
  },
  watch:{

    model(){
      this.skey ++;
    }
  }
}
</script>

<template>

<select @change="this.makeSelection" class="form-text-input form-select-main" v-model="selection" :key="skey">
  <option :value="null"> </option>
  <option v-for="option in filterInputCollection(this.data, param)" :key="option" :value="option">
    {{option.code ? `${option.code} ${option?.description ?? ''}` : (option.firstName ? `${option.firstName} ${option.lastName}`:``)}}
  </option>
</select>
</template>

<style scoped>

</style>