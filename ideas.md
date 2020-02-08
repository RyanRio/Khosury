# features
* clickers
* sliders
* ~~maybe guitar hero optional clicking thing~~ that's a slider
* rapid clicker instead of spinner
* **underwater levels**
* power ups (random?)
    * slow time
    * bonus score
    * fully getting special combo grants invincibility
    * kill streaks?
    * aim only power up
    * rhythm only power up
    * better power ups if struggling?

* no death mode
* score multiplier (streaks)
* health drain
* maybe hidden mode?
* tactical nuke after high enough streak?

# model
* note
    * time
    * position
    * local combo counter?
* sliders
    * time
    * speed
    * position(s)
    * 
* map
    * circle radius
    * approach rate
    * health drain speed
    * some kind of time tolerance for sliders
        * you can start it late
        * you can end it a bit off
        * you can go out of bounds for a bit

    * difficulty rating (auto-generated?)

    * shapes
    * sliders
    * rapid tappers
    * the song path
    * the background image? (path)


# interfaces
## note

## map
* getVisibleNotes(time)
* update on map


# view

# controller

# roles

## backend
* david, isi

## frontend
* else

scorekeeper
map
spawner
triangle game objects

triangle gameobject has
* id
* .../

map is a monobehavior
* tells spawner "yo, make a triangle here with this data"
* when it knows a triangle should die, it tells the spawner "yo, kill this guy"
    * it knows bc it has notes' time data
there is a triangle spawner
* spawner.size
* spawner.rate
* void spawner.spawnTriangle(pos, id)
    * needs to set triangle.id !!!
* void spawner.killTri(id)

# constants
* beatmap space: (0,0) to (16, 9) where (0,0) is bottom left
