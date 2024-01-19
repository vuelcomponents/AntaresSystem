export  class Planner {
    service;
    show = false;
    events = [
    /*
      start: '2018-11-16 10:00',
      end: '2018-11-20 12:37',
      title: 'Running Marathon',
      content: '<i class="icon material-icons">directions_run</i>',
      class: 'sport'
     */
    ];
    constructor(service){
       this.service = service;
    }
    addToEvents(event){
        this.events.push(event)
    }
    removeEvent(event){
        this.events = this.events.filter(e=> e?.id !== event?.id)
    }
    setVisibility(show){
        this.show = show
    }
    async create(plan){
        return this.service.create(plan);
    }
}