<script>
import {RecruitmentService} from "@/services/RecruitmentService";

export default {
  name: "RecruitmentScreen",
  setup(){
    const service = new RecruitmentService()
    return {
      service
    }
  }
}
</script>

<template>
Recruitment
</template>

<style scoped lang="scss">

</style>