<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Game extends Model
{
    //
	protected $fillable= ['token1', 'token1score', 'token2', 'token2score', 'chest1state', 'chest2state', 'chest3state', 'token1state', 'token2state', 'gamestate', 'room_id'];
	public $timestamps = false;
}
