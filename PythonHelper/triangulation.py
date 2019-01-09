import pyart

def main():
    xs = [1000, 1350, 1400, 1070, 900]
    ys = [250, 300, 700, 550, 400]

    if len(xs) != len(ys):
        print('len error.')
        return

    for pair in zip(xs, ys):
        print(pair)

    art = pyart.pyart()
    art.invert_yaxis()
    art.turn_axis_off()
    art.nextLabel = 'Hull'
    art.dots(xs, ys, color='r', zord=10)
    art.line(xs + [xs[0]], ys + [ys[0]], color='b')
    for (x,y) in zip(xs, ys):
        art.text(x, y, f'({x}; {y})', fontsize=12)
    art.save('plot.png', 500)
    art.show()

if __name__ == '__main__':
    main()
