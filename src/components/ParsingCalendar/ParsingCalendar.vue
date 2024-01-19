<script>
import moment from "moment";

export default{
  props:{
    disabled:undefined,
    model:undefined,
    setter:undefined
  },
  data(){
    return{
      dataModel:null,
    }
  },
  mounted(){
    if(this.model) {
      this.dataModel = moment(this.model).format("DD/MM/YYYY");
    }
  },
  methods:{
    change(){
      this.setter(this.dataModel)
    }
  }
}
</script>

<template>
  <Calendar :disabled="disabled" @date-select="change" date-format="yy-mm-dd" v-model="dataModel" class="form-text-input"/>
</template>

<style>

</style>