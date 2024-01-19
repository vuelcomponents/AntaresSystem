<script >
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";

export default{
  name:"MatchingStage",
  props:{
    employee:undefined,
     close:Function
  },
  methods:{

  }
}
</script>

<template>
<seciton class="stage-container" id="matching-stage-container" @click="close">
  <main>
    {{employee}}
  </main>
</seciton>

</template>

<style >

</style>