<?php

namespace App\Http\Controllers;

use App\Models\Room;
use App\Models\Game;
use Illuminate\Http\Request;
use Illuminate\Support\Str;
use Illuminate\Foundation\Testing\WithoutMiddleware;

class RoomController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\Room  $room
     * @return \Illuminate\Http\Response
     */
    public function show(Room $room)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\Room  $room
     * @return \Illuminate\Http\Response
     */
    public function edit(Room $room)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \App\Models\Room  $room
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Room $room)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\Room  $room
     * @return \Illuminate\Http\Response
     */
    public function destroy(Room $room)
    {
        //
    }
	public function joinRoom()
	{
		if(Room::where('roomstate', '=', '0')->get()->count()==0)
		{
			$room = new Room();
			$room->token1 = Str::random();
			$room->roomstate = 0;
			$room->started = 0;
			$room->finished = 0;
			$room->save();
			return response()->json(['token' => $room->token1, 'id' => $room->id, 'type' => 'master']);
		}
		else
		{
			$room = Room::where('roomstate', '=', '0')->first();
			$room->token2 = Str::random();
			while($room->token2 == $room->token1)
			{
				$room->token2 = Str::random();
			}
			$room->roomstate = 1;
			$room->save();
			return response()->json(['token' => $room->token2, 'id' => $room->id, 'type' => 'member']);
		}
		
		
	}
	public function roomData($id)
	{
		$room = Room::find($id);
		return response()->json(['data' => $room]);
	}
	public function startRoom($id)
	{
		$room = Room::find($id);
		$room->roomstate=2;
		$room->started=1;
		$game = new Game();
		$game->token1 = $room->token1;
		$game->token2 = $room->token2;
		$game->token1score = 0;
		$game->token2score = 0;
		$game->chest1state = 1;
		$game->chest2state = 1;
		$game->chest3state = 1;
		$game->gamestate = 0;
		$game->token1state = 0;
		$game->token2state = 0;
		$game->room_id = $room->id;
		$game->save();
		$room->save();
		return response()->json(['data' => $room]);
	}
}
