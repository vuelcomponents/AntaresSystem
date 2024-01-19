<script >
export default{
  props:{
    params:undefined
  },
  data(){
    return{
      advise:
          this.params.colDef.single ? this.params.data?.tempAdvise :
          this.params.data?.advises.find(a=>a.position?.code === this.params.colDef.headerName)
      ,
      chanceVariantCodes:[],
      lossVariantCodes:[]
    }
  },
  mounted(){

    this.chanceVariantCodes = [
      ...(this.advise?.matched?.map(match => match.variant.code) || []),
    ];
    this.lossVariantCodes = [
      ...(this.advise?.unmatched?.map(unmatch => unmatch.variant.code) || []),
    ];

  },
  methods:{
    hint(event){
      this.$refs.szymon.style.left = event.target.getBoundingClientRect().left - 500 + 'px'
      this.$refs.szymon.style.top = event.target.getBoundingClientRect().top - 120 +'px'
      document.body.appendChild(this.$refs.szymon)
    },
    hintOff(){
      if(document.body.contains(this.$refs.szymon))
      document.body.removeChild(this.$refs.szymon)
    }
  },
}
</script>

<template>
  <div >
<!--    {{advise.chance}}-->
    <ProgressBar style="margin-top:7px;" :value="advise?.noLossPercent" v-if="advise" @mouseenter="hint" @mouseout="hintOff"></ProgressBar>
  </div>
  <div ref="szymon" class="szymon">
    <label>
      No loss ratio ( {{advise?.noLossPercent ?? 0}}% )
      <ProgressBar style="margin-top:7px;" :value="advise?.noLossPercent" @mouseenter="hint" @mouseout="hintOff"></ProgressBar>
    </label>
    <label>
      Loss ratio ( {{advise?.withLossPercent ?? 0}}% )
      <ProgressBar style="margin-top:7px;" :value="advise?.withLossPercent" @mouseenter="hint" @mouseout="hintOff"></ProgressBar>
    </label>
    <label>
      <p>Loss: {{advise?.loss}}</p>
      <p>Chance: {{advise?.chance}}</p>
    </label>
    <label>
      <span :hidden="this.chanceVariantCodes?.length<1">
        <p>Matched:</p>
        {{this.chanceVariantCodes.stringify()}}
      </span>
        <span :hidden="this.lossVariantCodes.length<1">
        <p>Unmatched</p>
        {{this.lossVariantCodes.stringify()}}
    </span>
    </label>
  </div>
</template>

<style>
.szymon{
  width:500px;
  height:500px;
  z-index:999999999;
  background: rgba(255, 255, 255, 0.88);
  border:solid 1px #00000020;
  border-radius:8px;
  position:fixed;
  left:0;
  top:0;
  padding:2%;
}
</style>