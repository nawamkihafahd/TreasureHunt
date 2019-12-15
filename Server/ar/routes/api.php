<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::get('joinRoom', 'RoomController@joinRoom');
Route::get('roomData/{id}', 'RoomController@roomData');
Route::get('startRoom/{id}', 'RoomController@startRoom');

Route::get('connectGame/{id}/{token}/{type}', 'GameController@connectGame');
Route::get('startGame/{id}', 'GameController@startGame');
Route::get('endGame/{id}', 'GameController@endGame');
Route::get('gameData/{id}', 'GameController@gameData');
Route::get('obtainChest/{id}/{chest}/{type}/{score}', 'GameController@obtainChest');
Route::get('globalAttack/{id}/{type}', 'GameController@globalAttack');
Route::get('randomAttack/{id}/{type}', 'GameController@randomAttack');
Route::get('refreshChest/{id}', 'GameController@refreshChest');

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});
