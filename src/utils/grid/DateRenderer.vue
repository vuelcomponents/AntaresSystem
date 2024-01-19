<script >
import moment from "moment";

export default{
  name:'DateRenderer',
  props:{
    params:undefined
  },
  data(){
    return{
      date: null
    }
  },
  mounted(){
    if(this.params.value){
      this.date = moment(this.params.value).format("YYYY-MM-DD")
    }
  },
  methods:{
    change(){
      this.params.setValue(moment(this.date, "YYYY-MM-DD").format("YYYY-MM-DD"))
    }
  },
  watch:{}

}

</script>

<template>
  <Calendar date-format="yy-mm-dd" @date-select="change" v-model="date" class="form-text-input date-renderer"/>
</template>

<style>
.date-renderer{
  border:none!important;
  display:flex!important;
  font-size:1em;
  align-items:center!important;
}
</style>