<?php

namespace App\Http\Controllers;

use App\Models\Game;
use Illuminate\Http\Request;
use Illuminate\Foundation\Testing\WithoutMiddleware;

class GameController extends Controller
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
     * @param  \App\Models\Game  $game
     * @return \Illuminate\Http\Response
     */
    public function show(Game $game)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\Game  $game
     * @return \Illuminate\Http\Response
     */
    public function edit(Game $game)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \App\Models\Game  $game
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Game $game)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\Game  $game
     * @return \Illuminate\Http\Response
     */
    public function destroy(Game $game)
    {
        //
    }
	
	public function connectGame($id, $token, $type)
	{
		$game = Game::where('room_id', '=', $id)->first();
		if($type=='1')
		{
			$game->token1state=1;
		}
		else
		{
			$game->token2state=1;
		}
		$game->save();
		return response()->json(['hasil' => 'connected']);
	}
	public function gameData($id)
	{
		$game = Game::where('room_id', '=', $id)->first();
		return response()->json(['data' => $game]);
	}
	public function startGame($id)
	{
		$game = Game::where('room_id', '=', $id)->first();
		$game->gamestate = 1;
		$game->save();
		return response()->json(['data' => $game]);
	}
	public function endGame($id)
	{
		$game = Game::where('room_id', '=', $id)->first();
		$game->gamestate = 2;
		$game->save();
		return response()->json(['data' => $game]);
	}
	public function obtainChest($id, $chest, $type, $score)
	{
		$game = Game::where('room_id', '=', $id)->first();
		if($type == '1')
		{
			$game->token1score += $score;
		}
		else
		{
			$game->token2score += $score;
		}
		if($chest == '1')
		{
			$game->chest1state=0;
		}
		else if($chest == '2')
		{
			$game->chest2state=0;
		}
		else
		{
			$game->chest3state=0;
		}
		$game->save();
		return response()->json(['data' => $game]);
	}
	public function globalAttack($id, $type)
	{
		$game = Game::where('room_id', '=', $id)->first();
		if($type=='1')
		{
			$game->token2score -= 50;
		}
		else
		{
			$game->token1score -= 50;
		}
		$game->save();
		return response()->json(['data' => $game]);
	}
	public function randomAttack($id, $type)
	{
		$game = Game::where('room_id', '=', $id)->first();
		if($type=='1')
		{
			$game->token2score -= 50;
		}
		else
		{
			$game->token1score -= 50;
		}
		$game->save();
		return response()->json(['data' => $game]);
	}
	public function refreshChest($id)
	{
		$game = Game::where('room_id', '=', $id)->first();
		$game->chest1state = 1;
		$game->chest2state = 1;
		$game->chest3state = 1;
		$game->save();
		return response()->json(['data' => $game]);
	}
	
}
